using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.TierListMaking;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.Auth.Models;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.UserProfile.Features;

public record GetSelfProfileQuery : IRequest<Result<GetSelfProfileResponse>>;

public record GetSelfProfileResponse : BaseAuditedEntityResponse
{
	public string? Email { get; init; }
	public string? UserName { get; init; }

	public string Bio { get; init; }
	public string? AvatarUrl { get; init; }

	public List<TierList_SimpleResponse> TierLists { get; init; }
	public record TierList_SimpleResponse : BaseAuditedEntityResponse
	{
		public string Title { get; init; }
		public string? Description { get; init; }
		public TierListType Type { get; init; }

		public TierList_SimpleResponse(TierList tierList) : base(tierList)
			=> (Title, Description, Type) = (tierList.Title, tierList.Description, tierList.Type);
	}

	public GetSelfProfileResponse(AppUser user, UserProfile profile) : base(profile)
	{
		(Email, UserName, Bio, AvatarUrl) = (user.Email, user.UserName, profile.Bio, profile.AvatarUrl);

		TierLists = profile.TierLists
			.Select(tl => new TierList_SimpleResponse(tl))
			.ToList();
	}
}

class GetSelfProfileHandler : BaseHandler<GetSelfProfileQuery, GetSelfProfileResponse>
{
	public GetSelfProfileHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<GetSelfProfileResponse>> Handle(GetSelfProfileQuery query, CancellationToken cancellationToken)
	{
		var (user, role) = (await _authUtils.GetUserWithRole()).Value;

		var dbProfile = await _context.UserProfiles
			.Include(up => up.TierLists)
			.SingleOrDefaultAsync(up => up.UserId == user.Id);

		if (dbProfile is null)
		{
			return _resultFactory.NotFound("Profile not found");
		}

		var profile_Res = new GetSelfProfileResponse(user, dbProfile);

		return _resultFactory.Ok(profile_Res);
	}
}