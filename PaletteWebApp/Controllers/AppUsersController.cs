using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaletteWebApp.Models;
using PaletteWebApp.ViewModels;

namespace PaletteWebApp.Controllers;
public class AppUsersController : Controller
{
    private UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AppUsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View(_userManager.Users);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppUserViewModel appUserViewModel)
    {
        if (ModelState.IsValid)
        {
            AppUser newUser = new AppUser()
            {
                Email = appUserViewModel.Email,
                UserName = appUserViewModel.Name
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, appUserViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await _userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(appUserViewModel);
            }
        }
        else
        {
            return View(appUserViewModel);
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        AppUser? appUserToDelete = await _userManager.FindByIdAsync(id);
        if (appUserToDelete != null)
        {
            var result = await _userManager.DeleteAsync(appUserToDelete);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AddErrors(result);
            }
        }
        else
        {
            ModelState.AddModelError(String.Empty, "User not found");
        }
        return View("Index", _userManager.Users);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id)
    {
        AppUser? userToEdit = await _userManager.FindByIdAsync(id);

        if (userToEdit == null)
        {
            return View("NotFound");
        }
        else
        {

            AppUserViewModel appUserViewModel = new()
            {
                Email = userToEdit.Email ?? String.Empty,
                Name = userToEdit.UserName ?? String.Empty
            };
            return View(appUserViewModel);
        }
    }

    public async Task<IActionResult> EditActiveUser()
    {
        string? usersName = User?.Identity?.Name;
        if (usersName != null)
        {
            AppUser? userToEdit = await _userManager.FindByNameAsync(usersName);

            if (userToEdit != null)
            {
                AppUserViewModel appUserViewModel = new()
                {
                    Email = userToEdit.Email ?? String.Empty,
                    Name = userToEdit.UserName ?? String.Empty
                };
                return View(appUserViewModel);
            }
            else
            {
                return View("NotFound");
            }
        }
        else
        {
            return RedirectToAction("Create");
        }
    }
    private void AddErrors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError(String.Empty, error.Description);
    }
}
