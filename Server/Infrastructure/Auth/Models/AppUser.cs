using Microsoft.AspNetCore.Identity;
using Touhou_Songs.App.UserProfile;

namespace Touhou_Songs.Infrastructure.Auth.Models;

public class AppUser : IdentityUser
{
	public int? ProfileId { get; set; }
	public UserProfile? Profile { get; set; }

	public List<AppUserRole> UserRoles { get; set; } = new();

	public AppUser(string userName, string email) => (UserName, Email) = (userName, email);

	public void AddProfile()
	{
		var profile = new UserProfile()
		{
			UserId = Id,
			User = this,
		};

		Profile = profile;
	}
}
