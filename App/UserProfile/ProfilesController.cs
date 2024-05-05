using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.UserProfile.Features;
using Touhou_Songs.Infrastructure.API;

namespace Touhou_Songs.App.UserProfile;

[Authorize]
public class ProfilesController : ApiController
{
	private readonly ISender _sender;

	public ProfilesController(ISender sender)
	{
		_sender = sender;
	}

	[HttpPut("update")]
	public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
	{
		var res = await _sender.Send(command);
		return ToResponse(res);
	}
}
