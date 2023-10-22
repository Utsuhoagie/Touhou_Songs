using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Touhou_Songs.Features.Songs
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SongsController(IMediator mediator) => _mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> CreateSong(CreateSongRequest req)
		{
			await _mediator.Send(req);

			return Ok();
		}
	}
}
