using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Paging;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCirclesQuery(string? SearchName) : PagingParams, IRequest<Result<Paged<CircleResponse>>>;

public record CircleResponse : BaseAuditedEntityResponse
{
	public string Name { get; set; }
	public UnofficialStatus Status { get; set; }

	public required List<string> ArrangementSongTitles { get; set; }

	public CircleResponse(Circle circle) : base(circle)
		=> (Name, Status) = (circle.Name, circle.Status);
}

class GetCirclesHandler : BaseHandler<GetCirclesQuery, Paged<CircleResponse>>
{
	public GetCirclesHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<Paged<CircleResponse>>> Handle(GetCirclesQuery query, CancellationToken cancellationToken)
	{
		var getCirclesQuery = _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => query.SearchName == null || EF.Functions.ILike(c.Name, $"%{query.SearchName}%"))
			.OrderBy(c => c.Name);

		var circles_Res = await getCirclesQuery
			.Skip((query.Page - 1) * query.PageSize)
			.Take(query.PageSize)
			.Select(c => new CircleResponse(c)
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title).ToList(),
			})
			.ToListAsync();

		var totalCircles = await getCirclesQuery.CountAsync();

		var pagedCircles_Res = new Paged<CircleResponse>(query)
		{
			TotalItemsCount = totalCircles,
			Items = circles_Res,
		};

		return _resultFactory.Ok(pagedCircles_Res);
	}
}
