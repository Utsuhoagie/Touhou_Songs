using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs
{
	[Route("api/[controller]")]
	[ApiController]
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
	}
}
