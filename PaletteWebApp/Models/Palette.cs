using System.Diagnostics.CodeAnalysis;

namespace PaletteWebApp.Models;

public class Palette
{
	public int Id { get; set; }
	public List<uint>? Colors {  get; set; }
	public List<AppUser>? Users { get; set; }
}
