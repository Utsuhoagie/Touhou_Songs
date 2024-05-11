using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.UserProfile.Features;

// NOTE: null -> don't update
public class UpdateProfileCommand : IRequest<Result<string>>
{
	public string? Bio { get; set; }
	public string? AvatarUrl { get; set; }
}

class UpdateProfileHandler : BaseHandler<UpdateProfileCommand, string>
{
	public UpdateProfileHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public async override Task<Result<string>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
	{
		var (user, role) = (await _authUtils.GetUserWithRole()).Value;

		var dbProfile = await _context.UserProfiles.SingleOrDefaultAsync(up => up.UserId == user.Id);

		if (dbProfile is null)
		{
			return _resultFactory.NotFound("Profile not found");
		}

		dbProfile.Update(command.Bio, command.AvatarUrl);

		await _context.SaveChangesAsync();

		return _resultFactory.Ok("Profile updated");
	}
}
