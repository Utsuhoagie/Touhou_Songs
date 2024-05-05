using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.UserProfile.Features;

public class GetSelfProfileQuery : IRequest<Result<GetSelfProfileResponse>> { }

public class GetSelfProfileResponse
{
	public string Bio { get; set; }
	public string? AvatarUrl { get; set; }

	public GetSelfProfileResponse(string bio, string? avatarUrl) => (Bio, AvatarUrl) = (bio, avatarUrl);
}

class GetSelfProfileHandler : BaseHandler<GetSelfProfileQuery, GetSelfProfileResponse>
{
	public GetSelfProfileHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public async override Task<Result<GetSelfProfileResponse>> Handle(GetSelfProfileQuery query, CancellationToken cancellationToken)
	{
		var (user, role) = (await _authUtils.GetUserWithRole()).Value;

		var dbProfile = await _context.UserProfiles.SingleOrDefaultAsync(up => up.UserId == user.Id);

		if (dbProfile is null)
		{
			return _resultFactory.NotFound("Profile not found");
		}

		var profile_Resp = new GetSelfProfileResponse(dbProfile.Bio, dbProfile.AvatarUrl);

		return _resultFactory.Ok(profile_Resp);
	}
}