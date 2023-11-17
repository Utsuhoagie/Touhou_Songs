using Microsoft.AspNetCore.Identity;

namespace Touhou_Songs.Infrastructure.Auth
{
	public class AppUser : IdentityUser
	{
		public AppUser(string userName, string email) => (UserName, Email) = (userName, email);
	}
}
