using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.Infrastructure.Auth.Features
{
	public record RegisterAdminCommand : IRequest<RegisterAdminResponse>
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

	public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, RegisterAdminResponse>
	{
		private readonly Touhou_Songs_Context _context;
		private readonly UserManager<AppUser> _userManager;

		public RegisterAdminCommandHandler(Touhou_Songs_Context context, UserManager<AppUser> userManager) =>
			(_context, _userManager) = (context, userManager);

		public async Task<RegisterAdminResponse> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
		{
			var existingUser = await _userManager.FindByEmailAsync(command.Email);

			if (existingUser is not null)
			{
				throw new AppException(HttpStatusCode.Conflict, $"User with email {command.Email} already exists.");
			}

			var newUser = new AppUser(command.UserName, command.Email);
			await _userManager.CreateAsync(newUser, command.Password);
			await _userManager.AddToRoleAsync(newUser, AuthRoles.Admin);

			return new RegisterAdminResponse(command.UserName, command.Email);
		}
	}
}
