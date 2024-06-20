using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCirclesQuery(string? searchName) : IRequest<Result<IEnumerable<CircleResponse>>>;

public record CircleResponse : BaseAuditedEntityResponse
{
	public string Name { get; set; }
	public UnofficialStatus Status { get; set; }

	public required List<string> ArrangementSongTitles { get; set; }

	public CircleResponse(Circle circle) : base(circle)
		=> (Name, Status) = (circle.Name, circle.Status);
}

class GetCirclesHandler : BaseHandler<GetCirclesQuery, IEnumerable<CircleResponse>>
{
	public GetCirclesHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<CircleResponse>>> Handle(GetCirclesQuery query, CancellationToken cancellationToken)
	{
		var circles_Res = await _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
			.Select(c => new CircleResponse(c)
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title).ToList(),
			})
			.ToListAsync();

		return _resultFactory.Ok(circles_Res);
	}
}
