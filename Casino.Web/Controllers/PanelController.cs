using Casino.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Web.Controllers;

[Authorize]
public class PanelController : Controller
{
    [HttpGet("/admin")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public IActionResult AdminPanel()
    {
        return View();
    }

    [HttpGet("/bookmaker")]
    [Authorize(Roles = nameof(UserRole.Bookmaker))]
    public IActionResult BookmakerPanel()
    {
        return View();
    }

    [HttpGet("/customer")]
    [Authorize(Roles = nameof(UserRole.Customer))]
    public IActionResult CustomerPanel()
    {
        return View();
    }
}
