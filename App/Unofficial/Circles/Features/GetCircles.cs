using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCirclesQuery(string? searchName) : IRequest<Result<IEnumerable<CircleResponse>>>;

public record CircleResponse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Status { get; set; }

	public required IEnumerable<string> ArrangementSongTitles { get; set; }

	public CircleResponse(int id, string name, string status)
		=> (Id, Name, Status) = (id, name, status);
}

class GetCirclesHandler : BaseHandler<GetCirclesQuery, IEnumerable<CircleResponse>, Result<IEnumerable<CircleResponse>>>
{
	public GetCirclesHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<CircleResponse>>> Handle(GetCirclesQuery query, CancellationToken cancellationToken)
	{
		var circles_Res = await _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
			.Select(c => new CircleResponse(c.Id, c.Name, c.Status.ToString())
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title),
			})
			.ToListAsync();

		return Ok(circles_Res);
	}
}
