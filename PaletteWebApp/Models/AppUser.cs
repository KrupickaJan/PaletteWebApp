using Microsoft.AspNetCore.Identity;

namespace PaletteWebApp.Models;

public class AppUser : IdentityUser 
{
    public List<Palette> Palettes {  get; set; }
}

