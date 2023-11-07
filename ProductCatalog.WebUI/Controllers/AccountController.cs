﻿using ProductCatalog.Domain.Account;
using ProductCatalog.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.WebUI.Controllers;
public class AccountController : Controller
{
    private readonly IAuthenticate _authenticateService;

    public AccountController(IAuthenticate authenticateService)
    {
        _authenticateService = authenticateService;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authenticateService.RegisterUserAsync(model.Email, model.Password);
        if (result)
        {
            return Redirect("/");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return View(model);
        }
    }

    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authenticateService.AuthenticateAsunc(model.Email, model.Password);
        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(model.ReturnUrl);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
    }

    public async Task<IActionResult> Logout()
    {
        await _authenticateService.LogoutAsync();
        return Redirect("/Account/Login");
    }
}
