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

public class AutenticacaoController : Controller
{
    private readonly AppDbContext _context;
    private readonly LH_PET.Services.IUserService _userService;
    private readonly IConfiguration _config;

    public AutenticacaoController(AppDbContext context, LH_PET.Services.IUserService userService, IConfiguration config)
    {
        _context = context;
        _userService = userService;
        _config = config;
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

        try
        {
            await _userService.AddUserAsync(user, user.Password);
            TempData["Success"] = "Cadastro realizado com sucesso. Faça login.";
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Erro ao registrar usuário: " + ex.Message);
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
        var user = await _userService.GetByUsernameAsync(username);

        if (user != null && _userService.VerifyPassword(password, user.PasswordHash))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            TempData["Success"] = "Login efetuado com sucesso.";
            return RedirectToAction("Index", "Home");
        }

        TempData["Error"] = "Usuário ou senha inválidos.";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Token([FromForm] string username, [FromForm] string password)
    {
        var user = await _userService.GetByUsernameAsync(username);
        if (user == null || !_userService.VerifyPassword(password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        var jwtSection = _config.GetSection("Jwt");
        var key = jwtSection.GetValue<string>("Key") ?? string.Empty;
        var issuer = jwtSection.GetValue<string>("Issuer") ?? "LH_PET";
        var audience = jwtSection.GetValue<string>("Audience") ?? "LH_PET";
        var expireMinutes = jwtSection.GetValue<int?>("ExpireMinutes") ?? 60;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
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
