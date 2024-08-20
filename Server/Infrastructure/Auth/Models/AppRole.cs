using Microsoft.AspNetCore.Identity;

namespace Touhou_Songs.Infrastructure.Auth.Models;

public enum AuthRole
{
	Admin,
	User,
}

public class AppRole : IdentityRole
{
	public List<AppUserRole> UserRoles { get; set; } = new();
}