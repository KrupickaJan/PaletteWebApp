using System.ComponentModel.DataAnnotations;

namespace PaletteWebApp.ViewModels;

public class LoginViewModel
{
	[Required]
	public string? Email { get; set; }
	public string? Username { get; set; }
	public string? Password { get; set; }
	public string? ReturnUrl { get; set; }
	public bool RememberMe { get; set; }
}
