using Microsoft.AspNetCore.Mvc;

namespace Casino.Web.Controllers;

public class AccountController : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }
}
