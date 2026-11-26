using System.Diagnostics;
using Casino.Domain.Enums;
using Casino.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Web.Controllers;

public class HomeController : Controller
{
    // Hlavní obrazovka: stejná URL (/), ale jiný obsah podle role přihlášeného uživatele.
    public IActionResult Index()
    {
        if (User.IsInRole(nameof(UserRole.Admin)))
        {
            return View("Dashboard/Admin");
        }

        if (User.IsInRole(nameof(UserRole.Bookmaker)))
        {
            return View("Dashboard/Bookmaker");
        }

        return View("Dashboard/Customer");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
