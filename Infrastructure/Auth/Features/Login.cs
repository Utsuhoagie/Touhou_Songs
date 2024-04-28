using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Configuration;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.Infrastructure.Auth.Features;

public record LoginCommand : IRequest<LoginResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }

	public LoginCommand(string email, string password) => (Email, Password) = (email, password);
}

public record LoginResponse
{
	public string Token { get; set; }
	public DateTime ExpireAt { get; set; }

	public LoginResponse(string token, DateTime expireAt) => (Token, ExpireAt) = (token, expireAt);
}

class LoginCommandHandler : BaseHandler<LoginCommand, LoginResponse>
{
	private readonly UserManager<AppUser> _userManager;
	private readonly ConfigurationOptions _config;

	public LoginCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context, UserManager<AppUser> userManager, IOptions<ConfigurationOptions> options) : base(httpContextAccessor, context)
		=> (_userManager, _config) = (userManager, options.Value);

	public override async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByEmailAsync(command.Email);

		if (user is null)
		{
			throw new AppException(HttpStatusCode.NotFound, $"User with email {command.Email} not found.");
		}

		var checkPassword = await _userManager.CheckPasswordAsync(user, command.Password);

		if (!checkPassword)
		{
			throw new AppException(HttpStatusCode.Unauthorized);
		}

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, command.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		var userRoles = (await _userManager.GetRolesAsync(user)).ToList();

		userRoles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.JWT.Secret));

		var token = new JwtSecurityToken(
			issuer: _config.JWT.ValidIssuer,
			audience: _config.JWT.ValidAudience,
			expires: DateTime.Now.AddHours(_config.JWT.ValidHours),
			claims: claims,
			signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

		var tokenResponse = new JwtSecurityTokenHandler().WriteToken(token);

		return new LoginResponse(tokenResponse, token.ValidTo);
	}
}
