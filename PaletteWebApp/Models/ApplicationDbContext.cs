using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PaletteWebApp.Models;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
	public DbSet<Palette> Palettes { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
