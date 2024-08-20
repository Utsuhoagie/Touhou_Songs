using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Infrastructure.Auth.Models;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.Auth;

public class AuthUtils
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly UserManager<AppUser> _userManager;

	public AuthUtils(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		=> (_httpContextAccessor, _userManager) = (httpContextAccessor, userManager);

	public async Task<Result<(AppUser User, AuthRole Role)>> GetUserWithRole()
	{
		var resultFactory = new ResultFactory<(AppUser user, AuthRole role)>();

		var userFromClaims = _httpContextAccessor.HttpContext?.User;

		if (userFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		var userEmail = userFromClaims.FindFirstValue(ClaimTypes.Email);
		var userRoleString = userFromClaims.FindFirstValue(ClaimTypes.Role);

		if (userEmail is null || userRoleString is null)
		{
			return resultFactory.Unauthorized();
		}

		var userRole = Enum.Parse<AuthRole>(userRoleString);
		var dbUser = await _userManager
			.Users
			.Include(u => u.Profile)
			.SingleOrDefaultAsync(u => u.Email == userEmail);

		if (dbUser is null)
		{
			return resultFactory.Unauthorized();
		}

		return resultFactory.Ok((dbUser, userRole));
	}

	public Result<string> GetUserName()
	{
		var resultFactory = new ResultFactory<string>();

		var userFromClaims = _httpContextAccessor.HttpContext?.User;

		if (userFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		var userNameFromClaims = userFromClaims.FindFirstValue(ClaimTypes.Name);

		if (userNameFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		return resultFactory.Ok(userNameFromClaims);
	}

	public Result<bool> ValidateEntityBelongsToUser(BaseAuditedEntity entity)
	{
		var resultFactory = new ResultFactory<bool>();

		var userFromClaims = _httpContextAccessor.HttpContext?.User;

		if (userFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		var userName = userFromClaims.FindFirstValue(ClaimTypes.Name);
		var userEmail = userFromClaims.FindFirstValue(ClaimTypes.Email);
		var userRoleString = userFromClaims.FindFirstValue(ClaimTypes.Role);

		if (userName is null || userEmail is null || userRoleString is null)
		{
			return resultFactory.Unauthorized();
		}

		var entityBelongsToUser = userName == entity.CreatedByUserName;

		if (entityBelongsToUser)
		{
			return resultFactory.Ok(true);
		}
		else
		{
			return resultFactory.Forbidden("Not allowed to change other users' data");
		}
	}
}