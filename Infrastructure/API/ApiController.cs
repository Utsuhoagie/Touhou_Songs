using Microsoft.AspNetCore.Mvc;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.Infrastructure.API;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
	public IActionResult ToResponse<T>(Result<T> res) => StatusCode((int)res.StatusCode, res);
}
