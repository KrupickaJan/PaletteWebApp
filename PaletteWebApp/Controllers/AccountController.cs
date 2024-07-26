﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaletteWebApp.Models;
using PaletteWebApp.ViewModels;

namespace PaletteWebApp.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<AppUser> _userManager;
	private readonly SignInManager<AppUser> _signInManager;

	public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public IActionResult Login(string returnUrl)
	{
		LoginViewModel loginVm = new();
		loginVm.ReturnUrl = returnUrl;
		return View(loginVm);
	}

	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel login)
	{
		if (ModelState.IsValid)
		{
			AppUser? appUser = await _userManager.FindByEmailAsync(login.Email);
			if(appUser != null)
			{
				await _signInManager.SignOutAsync();
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, login.RememberMe, false);
				if (result.Succeeded)
				{
					return Redirect(login.ReturnUrl ?? "/");
				}
			}
			else
			{
				ModelState.AddModelError(nameof(login.Username), "Login failed: Invalid user name or password");
			}
		}
		return View(login);
	}
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
	public IActionResult AccessDenied()
	{
		return View();
	}

}