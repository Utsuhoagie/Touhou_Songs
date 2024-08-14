using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.App.DataManagement.Features;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.Infrastructure.API;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
	public IActionResult ToResponse<T>(Result<T> res) => StatusCode((int)res.StatusCode, res);

	public IActionResult ToResponse(Result<FileResponse> res) => res.Success
		? File(res.Value!.Contents, res.Value.MimeType, res.Value.FileName)
		: ToResponse(res);
}
