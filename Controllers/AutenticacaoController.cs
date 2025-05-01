using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using LH_PET.Context;
using LH_PET.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

public class AutenticacaoController : Controller
{
    private readonly AppDbContext _context;

    public AutenticacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Registro() => View();

    [HttpPost]
    public async Task<IActionResult> Registro(User user)
    {
        if (ModelState.IsValid)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        return View(user);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var hash = HashPassword(password);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);

        if (user != null)
        {

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Autentica o usuário
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Usuário ou senha inválidos.";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Autenticacao");
    }


    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

}
