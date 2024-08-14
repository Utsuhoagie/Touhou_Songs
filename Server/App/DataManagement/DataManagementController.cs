using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.DataManagement.Features;
using Touhou_Songs.Infrastructure.API;

namespace Touhou_Songs.App.DataManagement;

[Authorize]
public class DataManagementController : ApiController
{
	private readonly ISender _sender;

	public DataManagementController(ISender sender) => _sender = sender;

	[HttpGet("OfficialSongs")]
	public async Task<IActionResult> ExportAllOfficialSongs([FromQuery] ExportAllOfficialSongsQuery query)
	{
		var res = await _sender.Send(query);
		return ToResponse(res);
	}

	[HttpPost("OfficialSongs")]
	public async Task<IActionResult> ImportOfficialSongs([FromBody] ImportSongsOfGamesCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}

	[HttpPost("Characters")]
	public async Task<IActionResult> ImportCharacters([FromBody] ImportCharactersCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}

	[HttpPut("SongsContext")]
	public async Task<IActionResult> UpdateSongsContext([FromBody] UpdateSongContextsCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}

	[HttpPut("CharactersImageUrl")]
	public async Task<IActionResult> UpdateCharactersImageUrl([FromBody] UpdateCharacterImageUrlsCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}
}