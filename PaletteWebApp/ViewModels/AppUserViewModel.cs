using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PaletteWebApp.ViewModels;

public class AppUserViewModel
{
    [Required]
    public string Name {  get; set; }
    [Required]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
	[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
	public string ConfirmPassword { get; set; }
}
