using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using LH_PET.Context;
using LH_PET.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using LH_PET.Services;

public class AutenticacaoController : Controller
{
    private readonly AppDbContext _context;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;
    private readonly IRateLimitService _rateLimitService;
    private readonly IValidationService _validationService;
    private readonly IAuditService _auditService;

    public AutenticacaoController(
        AppDbContext context, 
        IUserService userService, 
        IConfiguration config,
        IRateLimitService rateLimitService,
        IValidationService validationService,
        IAuditService auditService)
    {
        _context = context;
        _userService = userService;
        _config = config;
        _rateLimitService = rateLimitService;
        _validationService = validationService;
        _auditService = auditService;
    }

    [HttpGet]
    public IActionResult Registro() => View();

    [HttpPost]
    public async Task<IActionResult> Registro(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        // Validação de senha forte
        if (!IsStrongPassword(user.Password))
        {
            ModelState.AddModelError("Password", "A senha precisa ter ao menos 8 caracteres, uma letra maiúscula, uma minúscula, um número e um caractere especial.");
            return View(user);
        }

        // Verifica username duplicado
        var existingUser = await _userService.GetByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "Este nome de usuário já está em uso.");
            return View(user);
        }

        // Verifica email duplicado
        var existingEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingEmail != null)
        {
            ModelState.AddModelError("Email", "Este email já está registerado.");
            return View(user);
        }

        try
        {
            await _userService.AddUserAsync(user, user.Password);
            
            // Log de auditoria
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            _auditService.LogUserRegistration(user.Username, user.Email);
            
            TempData["Success"] = "Cadastro realizado com sucesso. Faça login.";
            return RedirectToAction("Login");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Erro ao registrar usuário. Por favor, tente novamente.");
            return View(user);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        // Valida entrada
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            TempData["Error"] = "Usuário e senha são obrigatórios.";
            return View();
        }

        // Rate limiting: verifica se está bloqueado
        if (await _rateLimitService.IsLimitExceededAsync(username))
        {
            _auditService.LogRateLimitExceeded(username, ipAddress);
            TempData["Error"] = $"Muitas tentativas de login. Tente novamente em alguns minutos.";
            return View();
        }

        var user = await _userService.GetByUsernameAsync(username);

        if (user != null && _userService.VerifyPassword(password, user.PasswordHash))
        {
            // Sucesso: limpa tentativas
            await _rateLimitService.ClearAttemptsAsync(username);
            
            // Log de auditoria
            _auditService.LogLoginSuccess(username, ipAddress);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            TempData["Success"] = "Login efetuado com sucesso.";
            return RedirectToAction("Index", "Home");
        }

        // Falha: registra tentativa
        await _rateLimitService.RecordFailedAttemptAsync(username);
        var remainingAttempts = await _rateLimitService.GetRemainingAttemptsAsync(username);

        // Log de auditoria
        _auditService.LogLoginFailure(username, ipAddress, "Invalid credentials");

        TempData["Error"] = remainingAttempts > 0 
            ? $"Usuário ou senha inválidos. ({remainingAttempts} tentativas restantes)"
            : "Muitas tentativas de login. Tente novamente em alguns minutos.";
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Token([FromForm] string username, [FromForm] string password)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return BadRequest(new { message = "Usuário e senha são obrigatórios." });
        }

        // Rate limiting para API também
        if (await _rateLimitService.IsLimitExceededAsync(username))
        {
            _auditService.LogRateLimitExceeded(username, ipAddress);
            return StatusCode(429, new { message = "Too many login attempts. Please try again later." });
        }

        var user = await _userService.GetByUsernameAsync(username);
        if (user == null || !_userService.VerifyPassword(password, user.PasswordHash))
        {
            await _rateLimitService.RecordFailedAttemptAsync(username);
            _auditService.LogLoginFailure(username, ipAddress, "Invalid API credentials");
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        // Sucesso
        await _rateLimitService.ClearAttemptsAsync(username);
        _auditService.LogLoginSuccess(username, ipAddress);

        var jwtSection = _config.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key") ?? string.Empty;
        var issuer = jwtSection.GetValue<string>("Issuer") ?? "LH_PET";
        var audience = jwtSection.GetValue<string>("Audience") ?? "LH_PET";
        var expireMinutes = jwtSection.GetValue<int?>("ExpireMinutes") ?? 60;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            { 
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { token = tokenString });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var username = User.Identity?.Name ?? "Unknown";
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        
        _auditService.LogSensitiveOperation(username, "Logout", "Authentication");
        
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Autenticacao");
    }

    private bool IsStrongPassword(string? password)
    {
        if (string.IsNullOrEmpty(password)) return false;
        if (password.Length < 8) return false;
        if (!password.Any(char.IsUpper)) return false;
        if (!password.Any(char.IsLower)) return false;
        if (!password.Any(char.IsDigit)) return false;
        if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;
        return true;
    }
}
