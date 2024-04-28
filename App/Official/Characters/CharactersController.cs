using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.Characters.Features;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.Official.Characters;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

	[HttpGet("{Name}")]
	public async Task<IActionResult> GetCharacterDetail([FromRoute] GetCharacterDetailQuery query)
	{
		var characterResponse = await _sender.Send(query);

		return Ok(characterResponse);
	}

	[HttpPost]
	[Authorize(Roles = AuthRoles.Admin)]
	public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterCommand command)
	{
		await _sender.Send(command);

		return Ok();
	}
}
