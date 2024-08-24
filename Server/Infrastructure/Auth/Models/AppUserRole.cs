using Microsoft.AspNetCore.Identity;

namespace Touhou_Songs.Infrastructure.Auth.Models;

public class AppUserRole : IdentityUserRole<string>
{
	public AppUser User { get; set; } = default!;
	public AppRole Role { get; set; } = default!;
}
