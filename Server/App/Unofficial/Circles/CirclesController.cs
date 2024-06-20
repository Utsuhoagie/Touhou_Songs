using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Unofficial.Circles.Features;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.Unofficial.Circles;

public class CirclesController : ApiController
{
	private readonly ISender _sender;

	public CirclesController(ISender sender) => _sender = sender;

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetCircles([FromQuery] GetCirclesQuery query)
	{
		var res = await _sender.Send(query);

		return ToResponse(res);
	}

	[HttpGet("{Name}")]
	[Authorize]
	public async Task<IActionResult> GetCircleDetail([FromRoute] GetCircleDetailQuery query)
	{
		var res = await _sender.Send(query);

		return ToResponse(res);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateCircle([FromBody] CreateCircleCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}

	[HttpPut("{Name}/ValidateStatus")]
	[AuthorizeRoles(AuthRole.Admin)]
	public async Task<IActionResult> ValidateCircleStatus([FromRoute] string Name, [FromBody] ValidateCircleStatusPayload payload)
	{
		var command = new ValidateCircleStatusCommand(Name, payload);
		var res = await _sender.Send(command);

		return ToResponse(res);
	}
}
