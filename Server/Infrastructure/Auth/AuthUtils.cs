﻿using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Infrastructure.ExceptionHandling;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.Auth;

public class AuthUtils
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly UserManager<AppUser> _userManager;

	public AuthUtils(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
		=> (_httpContextAccessor, _userManager) = (httpContextAccessor, userManager);

	public async Task<Result<(AppUser user, AuthRole role)>> GetUserWithRole()
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
			throw new AppException(HttpStatusCode.Unauthorized);
			//return resultFactory.Unauthorized();
		}

		var userRole = Enum.Parse<AuthRole>(userRoleString);
		var user = await _userManager.FindByEmailAsync(userEmail);

		if (user is null)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
			//return resultFactory.Unauthorized();
		}

		return resultFactory.Ok((user, userRole));
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
}

/// <summary>
/// Should probably only be used for the DbContext
/// </summary>
public static class AuthUtilsStatic
{
	public static Result<string> GetUserName(IHttpContextAccessor httpContextAccessor)
	{
		var resultFactory = new ResultFactory<string>();

		var userFromClaims = httpContextAccessor.HttpContext?.User;

		if (userFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		var userNameFromClaims = userFromClaims.FindFirstValue(ClaimTypes.Name);

		if (userNameFromClaims is null)
		{
			//throw new AppException(HttpStatusCode.Unauthorized);
			return resultFactory.Unauthorized();
		}

		return resultFactory.Ok(userNameFromClaims);
	}
}