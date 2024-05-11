using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth.Features;

namespace Touhou_Songs.Infrastructure.Auth;

public class AuthController : ApiController
{
	private readonly ISender _sender;

	public AuthController(ISender sender) => _sender = sender;

	[HttpPost("Register")]
	public async Task<IActionResult> Register([FromBody] RegisterCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}

	[HttpPost("RegisterAdmin")]
	public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] LoginCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}
}
