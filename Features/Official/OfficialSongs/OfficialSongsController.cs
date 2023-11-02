using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Touhou_Songs.Features.Official.OfficialSongs
{
	[Route("api/[controller]")]
	[ApiController]
	public class OfficialSongsController : ControllerBase
	{
		private readonly ISender _sender;
		public OfficialSongsController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetOfficialSongs()
		{
			var officialSongResponses = await _sender.Send(new GetOfficialSongsQuery());

			return Ok(officialSongResponses);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOfficialSong(CreateOfficialSongCommand req)
		{
			await _sender.Send(req);

			return Ok();
		}
	}
}
