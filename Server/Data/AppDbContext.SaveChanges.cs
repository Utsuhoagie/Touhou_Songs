using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Infrastructure.Auth.Models;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Data;

public partial class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var currentUserNameResult = GetUserName();

		// Allow saving for non-auth actions
		// or for seeding test data
		if (!currentUserNameResult.Success)
		{
			return await base.SaveChangesAsync(cancellationToken);
		}

		var currentUserName = currentUserNameResult.Value!;

		var changedEntities = ChangeTracker
			.Entries()
			.Where(entity =>
				entity.Entity is BaseAuditedEntity
				&& (new[] { EntityState.Added, EntityState.Modified }).Contains(entity.State));

		foreach (var changedEntity in changedEntities)
		{
			BaseAuditedEntity entity = (BaseAuditedEntity)changedEntity.Entity;

			switch (changedEntity.State)
			{
				case EntityState.Added:
					entity.CreatedOn = DateTime.UtcNow;
					entity.CreatedByUserName = currentUserName;
					break;
				case EntityState.Modified:
					entity.UpdatedOn = DateTime.UtcNow;
					entity.UpdatedByUserName = currentUserName;
					break;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}

	private Result<string> GetUserName()
	{
		var resultFactory = new ResultFactory<string>();

		var userFromClaims = _httpContextAccessor.HttpContext?.User;

		var userNameFromClaims = userFromClaims?.FindFirstValue(ClaimTypes.Name);

		if (userFromClaims is null || userNameFromClaims is null)
		{
			return resultFactory.Unauthorized();
		}

		return resultFactory.Ok(userNameFromClaims);
	}
}
