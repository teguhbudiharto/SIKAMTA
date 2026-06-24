using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIKAMTA.Data;
using SIKAMTA.ViewModels;

namespace SIKAMTA.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _context.Users.FirstOrDefault(x =>
            x.Username == model.Username &&
            x.Password == model.Password);

        if (user == null)
        {
            ViewBag.Error = "Username atau Password salah";
            return View(model);
        }

        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetString("Nama", user.Nama);

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login");
    }
}