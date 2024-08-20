using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.Characters.Features;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.Auth.Models;

namespace Touhou_Songs.App.Official.Characters;

[Authorize]
public class CharactersController : ApiController
{
	private readonly ISender _sender;
	public CharactersController(ISender sender) => _sender = sender;

	[HttpGet]
	public async Task<IActionResult> GetCharacters([FromQuery] GetCharactersQuery query)
	{
		var characters_Res = await _sender.Send(query);

		return ToResponse(characters_Res);
	}

	[HttpGet("{Name}")]
	public async Task<IActionResult> GetCharacterDetail([FromRoute] GetCharacterDetailQuery query)
	{
		var character_Res = await _sender.Send(query);

		return ToResponse(character_Res);
	}

	[HttpPost]
	[AuthorizeRoles(AuthRole.Admin)]
	public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}
}
