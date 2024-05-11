using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.OfficialSongs.Features;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.Official.OfficialSongs;

[Authorize]
public class OfficialSongsController : ApiController
{
	private readonly ISender _sender;
	public OfficialSongsController(ISender sender) => _sender = sender;

	[HttpGet]
	public async Task<IActionResult> GetOfficialSongs([FromQuery] GetOfficialSongsQuery query)
	{
		var officialSongs_Res = await _sender.Send(query);

		return ToResponse(officialSongs_Res);
	}

	[HttpGet("{Id}")]
	public async Task<IActionResult> GetOfficialSongDetail([FromRoute] GetOfficialSongDetailQuery query)
	{
		var officialSong_Res = await _sender.Send(query);

		return ToResponse(officialSong_Res);
	}

	[HttpPost]
	[Authorize(Roles = AuthRoles.Admin)]
	public async Task<IActionResult> CreateOfficialSong([FromBody] CreateOfficialSongCommand req)
	{
		var res = await _sender.Send(req);

		return ToResponse(res);
	}
}
