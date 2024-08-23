using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.TierListMaking.Features;
using Touhou_Songs.Infrastructure.API;

namespace Touhou_Songs.App.TierListMaking;

[Authorize]
public class TierListsController : ApiController
{
	private readonly ISender _sender;

	public TierListsController(ISender sender) => _sender = sender;

	[HttpGet("Self")]
	public async Task<IActionResult> GetSelfTierLists([FromQuery] GetSelfTierListsQuery query)
	{
		var res = await _sender.Send(query);
		return ToResponse(res);
	}

	[HttpGet("{Id}")]
	public async Task<IActionResult> GetTierListDetail([FromRoute] GetTierListDetailQuery query)
	{
		var res = await _sender.Send(query);
		return ToResponse(res);
	}

	[HttpPost]
	public async Task<IActionResult> CreateTierList([FromBody] CreateTierListCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}

	[HttpPut("{Id}")]
	public async Task<IActionResult> UpdateTierListPlacements([FromRoute] int Id, [FromBody] UpdateTierListPlacementsPayload payload)
	{
		var command = new UpdateTierListPlacementsCommand(Id, payload);
		var res = await _sender.Send(command);
		return ToResponse(res);
	}
}
