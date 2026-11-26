using System.Security.Claims;
using Casino.Application.Auth;
using Casino.Domain.Models;
using Casino.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Web.Controllers;

public class AccountController : Controller
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRegisterUseCase _registerUseCase;

    public AccountController(ILoginUseCase loginUseCase, IRegisterUseCase registerUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
    }

    [AllowAnonymous]
    [HttpGet("/login")]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        return View(new LoginViewModel());
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _loginUseCase.LoginAsync(model.Email, model.Password, cancellationToken);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, result.Error!);
            return View(model);
        }

        await SignInAsync(result.User!, model.RememberMe);
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [AllowAnonymous]
    [HttpGet("/register")]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        return View(new RegisterViewModel());
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _registerUseCase.RegisterCustomerAsync(
            model.Email, model.Password, model.FirstName, model.LastName, model.DateOfBirth!.Value, cancellationToken);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        // Po úspěšné registraci uživatele rovnou přihlásíme.
        await SignInAsync(result.User!, isPersistent: false);
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost("/logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    private Task SignInAsync(AuthenticatedUser user, bool isPersistent)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        return HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = isPersistent });
    }
}
