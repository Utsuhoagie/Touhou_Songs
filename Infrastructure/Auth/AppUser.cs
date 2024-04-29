using Microsoft.AspNetCore.Identity;
using Touhou_Songs.App.UserProfile;

namespace Touhou_Songs.Infrastructure.Auth;

public class AppUser : IdentityUser
{
	public int? ProfileId { get; set; }
	public UserProfile? Profile { get; set; }

	public AppUser(string userName, string email) => (UserName, Email) = (userName, email);

	public void AddProfile(string displayName)
	{
		var profile = new UserProfile(displayName)
		{
			UserId = Id,
			User = this,
		};

		Profile = profile;
	}
}
