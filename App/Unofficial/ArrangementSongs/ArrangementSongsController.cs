﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ArrangementSongsController : ControllerBase
{
	private readonly ISender _sender;

	public ArrangementSongsController(ISender sender) => _sender = sender;

	[HttpGet]
	public async Task<IActionResult> GetArrangementSongs([FromQuery] GetArrangementSongsQuery query)
	{
		var res = await _sender.Send(query);

		return Ok(res);
	}

	[HttpGet("{Id}")]
	public async Task<IActionResult> GetArrangementSongDetail([FromRoute] GetArrangementSongDetailQuery query)
	{
		var res = await _sender.Send(query);

		return Ok(res);
	}

	[HttpPost]
	public async Task<IActionResult> CreateArrangementSong([FromBody] CreateArrangementSongCommand command)
	{
		var res = await _sender.Send(command);

		return Ok(res);
	}

	public class ValidateArrangementSongStatusBody
	{
		public string Status { get; set; } = default!;
	}
	[HttpPut("{Id}/ValidateStatus")]
	public async Task<IActionResult> ValidateArrangementSongStatus([FromRoute] int Id, [FromBody] ValidateArrangementSongStatusBody body)
	{
		var command = new ValidateArrangementSongStatusCommand(Id, body.Status);
		var res = await _sender.Send(command);

		return Ok(res);
	}
}
