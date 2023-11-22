using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Unofficial.Circles.Features;

namespace Touhou_Songs.App.Unofficial.Circles
{
	[Route("api/[controller]")]
	[ApiController]
	public class CirclesController : ControllerBase
	{
		private readonly ISender _sender;

		public CirclesController(ISender sender) => _sender = sender;

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetCircles([FromQuery] GetCirclesQuery query)
		{
			var res = await _sender.Send(query);

			return Ok(res);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateCircle([FromBody] CreateCircleCommand command)
		{
			var res = await _sender.Send(command);

			return Ok(res);
		}
	}
}
