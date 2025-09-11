using EmailApp.Entites;
using EmailApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailApp.Controllers;

public class LoginController (UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager): Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Bu mail sistemde kayıtlı değil.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Mail adresi veya şifre hatalı.");
            return View(model);
        }

        return RedirectToAction("Inbox", "Message");
    }
}
