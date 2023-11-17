using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.Infrastructure.Auth.Features;

namespace Touhou_Songs.Infrastructure.Auth
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ISender _sender;

		public AuthController(ISender sender) => _sender = sender;

		[HttpPost("RegisterAdmin")]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminCommand command)
		{
			var res = await _sender.Send(command);

			return Ok(res);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{
			var res = await _sender.Send(command);

			return Ok(res);
		}
	}
}
