using MediatR;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth.Models;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.Auth.Features;

public record ChangePasswordCommand : IRequest<Result<ChangePasswordResponse>>
{
	public string OldPassword { get; set; }
	public string NewPassword { get; set; }

	public ChangePasswordCommand(string oldPassword, string newPassword) =>
		(OldPassword, NewPassword) = (oldPassword, newPassword);
}

public record ChangePasswordResponse;

class ChangePasswordHandler : BaseHandler<ChangePasswordCommand, ChangePasswordResponse>
{
	private readonly UserManager<AppUser> _userManager;

	public ChangePasswordHandler(AuthUtils authUtils, AppDbContext context, UserManager<AppUser> userManager) : base(authUtils, context)
		=> _userManager = userManager;

	public async override Task<Result<ChangePasswordResponse>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
	{
		var dbCurrentUserWithRoleResult = await _authUtils.GetUserWithRole();

		if (!dbCurrentUserWithRoleResult.Success)
		{
			return _resultFactory.Unauthorized();
		}

		var user = dbCurrentUserWithRoleResult.Value.User;

		var changePasswordResult = await _userManager.ChangePasswordAsync(user, command.OldPassword, command.NewPassword);

		if (!changePasswordResult.Succeeded)
		{
			var errors = changePasswordResult.Errors.Select(e => $"{e.Code} - {e.Description}");
			return _resultFactory.Unauthorized(messages: errors);
		}

		return _resultFactory.Ok(new(), AuthI18n.ChangedPasswordSuccess.ToLanguage(Lang.EN));
	}
}