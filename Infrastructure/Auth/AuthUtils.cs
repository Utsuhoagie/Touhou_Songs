using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.Infrastructure.Auth;

public class AuthUtils
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly UserManager<AppUser> _userManager;

	public AuthUtils(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		=> (_httpContextAccessor, _userManager) = (httpContextAccessor, userManager);

	public async Task<(AppUser? user, string role)> GetUserWithRole()
	{
		var userFromClaims = _httpContextAccessor.HttpContext?.User;

		if (userFromClaims is null)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
		}

		var userEmail = userFromClaims.FindFirstValue(ClaimTypes.Email);
		var userRole = userFromClaims.FindFirstValue(ClaimTypes.Role);

		if (userEmail is null || userRole is null)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
		}

		var user = await _userManager.FindByEmailAsync(userEmail);

		if (user is null)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
		}

		return (user, userRole);
	}
}
