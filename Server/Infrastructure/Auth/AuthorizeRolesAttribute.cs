﻿using Microsoft.AspNetCore.Authorization;
using Touhou_Songs.Infrastructure.Auth.Models;

namespace Touhou_Songs.Infrastructure.Auth;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
	public AuthorizeRolesAttribute(AuthRole role, params AuthRole[] roles)
	{
		var allRoles = roles.ToList();
		allRoles.Add(role);

		var roleNames = allRoles.Select(role => Enum.GetName(role));

		Roles = string.Join(",", roleNames);
	}
}
