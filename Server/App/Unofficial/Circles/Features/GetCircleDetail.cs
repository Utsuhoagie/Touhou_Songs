using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCircleDetailQuery(string Name) : IRequest<Result<CircleResponse>>;

class GetCircleDetailHandler : BaseHandler<GetCircleDetailQuery, CircleResponse>
{
	public GetCircleDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<CircleResponse>> Handle(GetCircleDetailQuery query, CancellationToken cancellationToken)
	{
		var circle_Res = await _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => c.Name == query.Name)
			.Select(c => new CircleResponse(c)
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title).ToList(),
			})
			.SingleOrDefaultAsync();

		if (circle_Res is null)
		{
			return _resultFactory.NotFound($"Circle {query.Name} not found.");
		}

		return _resultFactory.Ok(circle_Res);
	}
}
