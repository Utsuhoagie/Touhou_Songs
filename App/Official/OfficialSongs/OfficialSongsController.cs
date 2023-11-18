using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.OfficialSongs.Features;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.Official.OfficialSongs
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OfficialSongsController : ControllerBase
	{
		private readonly ISender _sender;
		public OfficialSongsController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetOfficialSongs([FromQuery] GetOfficialSongsQuery query)
		{
			var officialSongResponses = await _sender.Send(query);

			return Ok(officialSongResponses);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOfficialSongDetail([FromRoute] GetOfficialSongDetailQuery query)
		{
			var officialSongResponse = await _sender.Send(query);

			return Ok(officialSongResponse);
		}

		[HttpPost]
		[Authorize(Roles = AuthRoles.Admin)]
		public async Task<IActionResult> CreateOfficialSong([FromBody] CreateOfficialSongCommand req)
		{
			await _sender.Send(req);

			return Ok();
		}
	}
}
