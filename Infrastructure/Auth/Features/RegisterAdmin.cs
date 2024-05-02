using MediatR;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.Infrastructure.Auth.Features;

public record RegisterAdminCommand : IRequest<Result<RegisterAdminResponse>>
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	public RegisterAdminCommand(string userName, string email, string password) =>
		(UserName, Email, Password) = (userName, email, password);
}

public record RegisterAdminResponse
{
	public string UserName { get; set; }
	public string Email { get; set; }

	public RegisterAdminResponse(string userName, string email) =>
		(UserName, Email) = (userName, email);
}

class RegisterAdminHandler : BaseHandler<RegisterAdminCommand, RegisterAdminResponse, Result<RegisterAdminResponse>>
{
	private readonly UserManager<AppUser> _userManager;

	public RegisterAdminHandler(AuthUtils authUtils, Touhou_Songs_Context context, UserManager<AppUser> userManager) : base(authUtils, context)
		=> _userManager = userManager;

	public override async Task<Result<RegisterAdminResponse>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
	{
		var existingUser = await _userManager.FindByEmailAsync(command.Email);

		if (existingUser is not null)
		{
			return Conflict($"User with email {command.Email} already exists.");
		}

		var newUser = new AppUser(command.UserName, command.Email);
		await _userManager.CreateAsync(newUser, command.Password);
		await _userManager.AddToRoleAsync(newUser, AuthRoles.Admin);

		return Ok(new RegisterAdminResponse(newUser.UserName!, newUser.Email!));
	}
}
