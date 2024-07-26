using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaletteWebApp.Models;
using PaletteWebApp.Services;
using PaletteWebApp.ViewModels;

namespace PaletteWebApp.Controllers;

public class PalettesController : Controller
{
    private PaletteService _paletteService;
    private UserManager<AppUser> _userManager;


    public PalettesController(PaletteService paletteService, UserManager<AppUser> userManager)
    {
        _paletteService = paletteService;
        _userManager = userManager;
    }

    public IActionResult Index(int page)
    {
        var listOfPalettes = _paletteService.GetPalettes();

        const int pageSize = 20;
        if (page < 1)
        {
            page = 1;
        }

        int recsCOunt = listOfPalettes.Count();
        Pager pager = new(recsCOunt, page, pageSize);
        int recSkip = (page - 1) * pageSize;
        var data = listOfPalettes.Skip(recSkip).Take(pager.PageSize).ToList();
        this.ViewBag.Pager = pager;

        return View(data);
    }

    [Authorize]
    public async Task<IActionResult> YourPalettes(int page)
    {

        var user = await _userManager.GetUserAsync(User);

        var listOfPalettes = await _paletteService.GetUsersPalettes(user?.UserName ?? String.Empty);

        const int pageSize = 20;
        if (page < 1)
        {
            page = 1;
        }

        int recsCOunt = listOfPalettes.Count();
        Pager pager = new(recsCOunt, page, pageSize);
        int recSkip = (page - 1) * pageSize;
        var data = listOfPalettes.Skip(recSkip).Take(pager.PageSize).ToList();
        this.ViewBag.Pager = pager;

        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult ShowPalette()
    {
        return View();
    }
    [HttpPost]
    public IActionResult UploadImage(IFormFile file, int paletteSize)
    {

        if (file != null && file.Length > 0 && _paletteService.AcceptableTypes.Contains(file.ContentType))
        {
            return View("ShowPalette", _paletteService.CreatePalette(file, paletteSize));
        }
        return View("WrongFileFormat");
    }
    [HttpPost]
    public async Task<IActionResult> UploadPalette(PaletteViewModel paletteViewModel)
    {
        string usersName = User?.Identity?.Name ?? String.Empty;
        await _paletteService.UploadPaletteAsync(paletteViewModel, usersName);
        if (!usersName.Equals(String.Empty))
        {
            return RedirectToAction("YourPalettes");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SavePalette(int paletteId, int page)
    {
        string usersName = User?.Identity?.Name ?? String.Empty;
        await _paletteService.SavePaletteToUsersList(paletteId, usersName);
        return RedirectToAction("Index", new { page });
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> RemoveFromUsersList(int paletteId, int page)
    {
        string usersName = User?.Identity?.Name ?? String.Empty;
        await _paletteService.RemovePaletteFromUsersList(paletteId, usersName);
        return RedirectToAction("YourPalettes", new { page });
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Delete(int paletteId, int page)
    {
        await _paletteService.RemovePaletteAsync(paletteId);
        return RedirectToAction("Index", new { page });
    }
}
