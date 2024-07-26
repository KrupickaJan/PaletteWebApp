using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaletteWebApp.Models;

namespace PaletteWebApp.Controllers;

[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
	private RoleManager<IdentityRole> _roleManager;
	private UserManager<AppUser> _userManager;

	public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
	{
		_roleManager = roleManager;
		_userManager = userManager;
	}


	public IActionResult Index()
	{
		return View(_roleManager.Roles);
	}

	public IActionResult Create()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Create(string name)
	{
		if (ModelState.IsValid)
		{
			IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}
			else
			{
				AddErrors(result);
			}
		}
		return View(name);
	}

	public async Task<IActionResult> Edit(string id)
	{
		IdentityRole? role = await _roleManager.FindByIdAsync(id);
		if (role != null && role.Name != null)
		{
			List<AppUser> members = new List<AppUser>();
			List<AppUser> nonmembers = new List<AppUser>();
			foreach (AppUser user in _userManager.Users)
			{
				var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
				list.Add(user);
			}
			return View(new RoleEdit
			{
				Role = role,
				RoleMembers = members,
				RoleNonMembers = nonmembers
			});
		}
		else
		{
			return View("NotFound");
		}

	}
	[HttpPost]
	public async Task<IActionResult> Edit(RoleModification roleModification)
	{
		AppUser? user;
		IdentityResult result;
        foreach (var id in roleModification.AddIds ?? new string[] { })
        {
            user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                result = await _userManager.AddToRoleAsync(user, roleModification.RoleName);
                if (result != IdentityResult.Success)
                {
                    AddErrors(result);
                }
            }
        }
        foreach (var id in roleModification.DeleteIds ?? new string[] { })
        {
            user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                result = await _userManager.RemoveFromRoleAsync(user, roleModification.RoleName);
                if (result != IdentityResult.Success)
                {
                    AddErrors(result);
                }
            }
        }
        return RedirectToAction("Index");
	}
	private void AddErrors(IdentityResult identityResult)
	{
		foreach (var error in identityResult.Errors)
		{
			ModelState.AddModelError(String.Empty, error.Description);
		}
	}
}
