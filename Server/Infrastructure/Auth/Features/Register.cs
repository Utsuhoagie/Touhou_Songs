using MediatR;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.Auth.Features;

public record RegisterCommand : IRequest<Result<RegisterResponse>>
{
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

	public RegisterCommand(string userName, string email, string password) =>
		(UserName, Email, Password) = (userName, email, password);
}

public record RegisterResponse
{
	public string UserName { get; set; }
	public string Email { get; set; }

	public RegisterResponse(string userName, string email) =>
		(UserName, Email) = (userName, email);
}

class RegisterHandler : BaseHandler<RegisterCommand, RegisterResponse>
{
	private readonly UserManager<AppUser> _userManager;

	public RegisterHandler(AuthUtils authUtils, Touhou_Songs_Context context, UserManager<AppUser> userManager) : base(authUtils, context)
		=> _userManager = userManager;

	public override async Task<Result<RegisterResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
	{
		var existingUser = await _userManager.FindByEmailAsync(command.Email);

		if (existingUser is not null)
		{
			return _resultFactory.Conflict($"User with email {command.Email} already exists.");
		}

		var newUser = new AppUser(command.UserName, command.Email);
		await _userManager.CreateAsync(newUser, command.Password);
		await _userManager.AddToRoleAsync(newUser, Enum.GetName(AuthRole.User)!);

		newUser.AddProfile();

		await _context.SaveChangesAsync();

		return _resultFactory.Ok(new RegisterResponse(newUser.UserName!, newUser.Email!));
	}
}
