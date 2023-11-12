using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.OfficialGames.Features;

namespace Touhou_Songs.App.Official.OfficialGames
{
	[Route("api/[controller]")]
	[ApiController]
	public class OfficialGamesController : ControllerBase
	{
		private readonly ISender _sender;
		public OfficialGamesController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetOfficialGames([FromQuery] GetOfficialGamesQuery query)
		{
			var officialGameResponses = await _sender.Send(query);

			return Ok(officialGameResponses);
		}

		[HttpGet("{GameCode}")]
		public async Task<IActionResult> GetOfficialGameDetail([FromRoute] GetOfficialGameDetailQuery query)
		{
			var officialGameDetailResponse = await _sender.Send(query);

			return Ok(officialGameDetailResponse);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOfficialGame([FromBody] CreateOfficialGameCommand command)
		{
			await _sender.Send(command);

			return Ok();
		}
	}
}
