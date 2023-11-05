using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.OfficialSongs.Features;

namespace Touhou_Songs.App.Official.OfficialSongs
{
	[Route("api/[controller]")]
	[ApiController]
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

		[HttpPost]
		public async Task<IActionResult> CreateOfficialSong([FromBody] CreateOfficialSongCommand req)
		{
			await _sender.Send(req);

			return Ok();
		}
	}
}
