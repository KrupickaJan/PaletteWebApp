using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaletteWebApp.Models;
using System.Diagnostics;

namespace PaletteWebApp.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private UserManager<AppUser> _userManager;

	public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
	{
		_logger = logger;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		AppUser? signedInUser = await _userManager.GetUserAsync(HttpContext.User);
		if (signedInUser == null)
		{
			return View("Index");
		}
		return View("Index", signedInUser.UserName);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
	[Route("/StatusCodeError/{statusCode}")]
	public IActionResult StatusCodeError(int statusCode)
	{
		if (statusCode == 404)
		{
			ViewBag.ErrorMessage = "404 Page Not Found.";
		}
		return View();
	}

}
