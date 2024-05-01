using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Unofficial.ArrangementSongs.Features;
using Touhou_Songs.Infrastructure.API;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs;

[Authorize]
public class ArrangementSongsController : ApiController
{
	private readonly ISender _sender;

	public ArrangementSongsController(ISender sender) => _sender = sender;

	[HttpGet]
	public async Task<IActionResult> GetArrangementSongs([FromQuery] GetArrangementSongsQuery query)
	{
		var res = await _sender.Send(query);

		return ToResponse(res);
	}

	[HttpGet("{Id}")]
	public async Task<IActionResult> GetArrangementSongDetail([FromRoute] GetArrangementSongDetailQuery query)
	{
		var res = await _sender.Send(query);

		return ToResponse(res);
	}

	[HttpPost]
	public async Task<IActionResult> CreateArrangementSong([FromBody] CreateArrangementSongCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}

	public record ValidateArrangementSongStatusBody(string Status);
	[HttpPut("{Id}/ValidateStatus")]
	public async Task<IActionResult> ValidateArrangementSongStatus([FromRoute] int Id, [FromBody] ValidateArrangementSongStatusBody body)
	{
		var command = new ValidateArrangementSongStatusCommand(Id, body.Status);
		var res = await _sender.Send(command);

		return ToResponse(res);
	}
}
