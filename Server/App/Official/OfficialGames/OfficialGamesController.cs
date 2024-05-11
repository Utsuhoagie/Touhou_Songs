using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.Official.OfficialGames.Features;
using Touhou_Songs.Infrastructure.API;
using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.Official.OfficialGames;

[Authorize]
public class OfficialGamesController : ApiController
{
	private readonly ISender _sender;
	public OfficialGamesController(ISender sender) => _sender = sender;

	[HttpGet]
	public async Task<IActionResult> GetOfficialGames([FromQuery] GetOfficialGamesQuery query)
	{
		var officialGames_Res = await _sender.Send(query);

		return ToResponse(officialGames_Res);
	}

	[HttpGet("{GameCode}")]
	public async Task<IActionResult> GetOfficialGameDetail([FromRoute] GetOfficialGameDetailQuery query)
	{
		var officialGameDetail_Res = await _sender.Send(query);

		return ToResponse(officialGameDetail_Res);
	}

	[HttpPost]
	[AuthorizeRoles(AuthRole.Admin)]
	public async Task<IActionResult> CreateOfficialGame([FromBody] CreateOfficialGameCommand command)
	{
		var res = await _sender.Send(command);

		return ToResponse(res);
	}
}
