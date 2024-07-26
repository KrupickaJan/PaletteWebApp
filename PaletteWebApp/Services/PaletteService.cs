using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaletteGeneratorLibrary;
using PaletteWebApp.Models;
using PaletteWebApp.ViewModels;
using SkiaSharp;

namespace PaletteWebApp.Services;

public class PaletteService
{
    const int DEFAULT_SIZE_OF_PALETTE = 5;
    private readonly string[] _acceptableTypes =
    {
        "image/bmp",   // BMP
        "image/gif",   // GIF
        "image/x-exif", // EXIF
        "image/jpeg",  // JPG/JPEG
        "image/png",   // PNG
        "image/tiff"   // TIFF
    };
    private ApplicationDbContext _dbContext;
    private UserManager<AppUser> _userManager;

	public PaletteService(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
	{
		_dbContext = dbContext;
        _userManager = userManager;
	}

    public string[] AcceptableTypes { get => _acceptableTypes; }

    internal PaletteViewModel CreatePalette(IFormFile file, int paletteSize) 
    {
        if(paletteSize == 0)
        {
            paletteSize = DEFAULT_SIZE_OF_PALETTE;
        }

        using var stream = file.OpenReadStream();
        stream.Position = 0;
        using SKBitmap bitmap = SKBitmap.Decode(stream);
        PaletteGenerator paletteGenerator = new(bitmap, paletteSize);

        List<ColorViewModel> VMcolors = new();

        paletteGenerator.Palette.ForEach(colorPoint => VMcolors.Add(new ColorViewModel() 
        {
            R = colorPoint.Color.Red,
            G = colorPoint.Color.Green,
            B = colorPoint.Color.Blue 
        }));

        PaletteViewModel imagePaletteVM = new()
        {
            Colors = VMcolors
        };
        return imagePaletteVM;
    }

    internal IEnumerable<PaletteViewModel> GetPalettes()
    {
        List<PaletteViewModel> palettesViewModels = new List<PaletteViewModel>();
        var allPalettes = _dbContext.Palettes.OrderByDescending(palette => palette.Id);
        allPalettes.ToList().ForEach(palette => palettesViewModels.Add(ModelToViewModel(palette)));
        return palettesViewModels;
    }

	internal  async Task<IEnumerable<PaletteViewModel>> GetUsersPalettes(string userName)
	{
        AppUser user = await _userManager.Users.Include(User => User.Palettes).FirstOrDefaultAsync(user => user.UserName == userName) ?? new AppUser();
		List<PaletteViewModel> palettesViewModels = new();
        user.Palettes.ForEach(palette => palettesViewModels.Add(ModelToViewModel(palette)));
		return palettesViewModels.OrderByDescending(palette => palette.Id);
	}

    internal async Task SavePaletteToUsersList(int paletteId, string userName)
    {
        AppUser? user = await _userManager.Users.Include(User => User.Palettes).FirstOrDefaultAsync(user => user.UserName == userName);
        if(user != null)
        {
            Palette paletteToSave = _dbContext.Palettes.FirstOrDefault(palette => palette.Id == paletteId) ?? new Palette() { Colors = new() };
            user.Palettes.Add(paletteToSave);
            _dbContext.SaveChanges();
        }
	}

    internal async Task RemovePaletteFromUsersList(int paletteId, string usersName)
    {
        AppUser? user = await _userManager.Users.Include(User => User.Palettes).FirstOrDefaultAsync(user => user.UserName == usersName);
        if (user != null)
        {
            Palette paletteToRemove = _dbContext.Palettes.FirstOrDefault(palette => palette.Id == paletteId) ?? new Palette() { Colors = new() };
            user.Palettes.Remove(paletteToRemove);
            _dbContext.SaveChanges();
        }
    }

    internal async Task UploadPaletteAsync(PaletteViewModel paletteViewModel, string usersName)
    {
        await SavePaletteToDatabaseAsync(paletteViewModel);
        if(!usersName.Equals(String.Empty))
        {
            await SavePaletteToUsersList(_dbContext.Palettes.OrderByDescending(p => p.Id).FirstOrDefault().Id, usersName);
        }
    }

    internal async Task SavePaletteToDatabaseAsync(PaletteViewModel paletteViewModel)
    {
        Palette palette = new()
        {
            Colors = new List<uint>()
        };
        paletteViewModel?.Colors?.ForEach(color => palette.Colors.Add(ColorViewModelToUint(color)));
        await _dbContext.Palettes.AddAsync(palette);
        _dbContext.SaveChanges();
    }

    private uint ColorViewModelToUint(ColorViewModel color)
    {
        uint unsignedInteger = ((uint)color.R << 16) + ((uint)color.G << 8) + (uint)color.B;

        return unsignedInteger;
    }

    private ColorViewModel UintToColorViewModel(uint color)
    {
        ColorViewModel colorViewModel = new ColorViewModel
        {
            R = (int)((color & 0x00FF0000) >> 16),
            G = (int)((color & 0x0000FF00) >> 8),
            B = (int)(color & 0x000000FF),
        };
        return colorViewModel;
    }

    private PaletteViewModel ModelToViewModel(Palette palette)
    {
        PaletteViewModel paletteViewModel = new()
        {
            Colors = new List<ColorViewModel>(),
            Id = palette.Id
        };
        palette?.Colors?.ForEach(color => paletteViewModel.Colors.Add(UintToColorViewModel(color)));
        return paletteViewModel;
    }

	internal async Task RemovePaletteAsync(int id)
	{
        Palette? paletteToRemove = await _dbContext.Palettes.FirstOrDefaultAsync(palette => palette.Id == id);
		if(paletteToRemove != null)
        {
			_dbContext.Palettes.Remove(paletteToRemove);
            await _dbContext.SaveChangesAsync();
        }
	}
}
