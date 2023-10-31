using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Touhou_Songs.Features.Official.OfficialGames
{
	[Route("api/[controller]")]
	[ApiController]
	public class OfficialGamesController : ControllerBase
	{
		private readonly ISender _sender;
		public OfficialGamesController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetOfficialGames()
		{
			var officialSongResponses = await _sender.Send(new GetOfficialGamesQuery());

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
