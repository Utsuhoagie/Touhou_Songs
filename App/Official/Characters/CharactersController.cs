using MediatR;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.Characters.Features;

namespace Touhou_Songs.App.Official.Characters
{
	[Route("api/[controller]")]
	[ApiController]
	public class CharactersController : ControllerBase
	{
		private readonly ISender _sender;
		public CharactersController(ISender sender) => _sender = sender;

		[HttpGet]
		public async Task<IActionResult> GetCharacters([FromQuery] GetCharactersQuery query)
		{
			var characterResponses = await _sender.Send(query);

			return Ok(characterResponses);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterCommand command)
		{
			await _sender.Send(command);

			return Ok();
		}
	}
}
