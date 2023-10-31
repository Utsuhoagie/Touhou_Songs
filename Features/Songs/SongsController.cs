using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Touhou_Songs.Features.Songs
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongsController : ControllerBase
	{
		private readonly ISender _sender;
		public SongsController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetSongs()
		{
			var songResponses = await _sender.Send(new GetSongsQuery());

			return Ok(songResponses);
		}

		[HttpPost]
		public async Task<IActionResult> CreateSong(CreateSongCommand req)
		{
			await _sender.Send(req);

			return Ok();
		}
	}
}
