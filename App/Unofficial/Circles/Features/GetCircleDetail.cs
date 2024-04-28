using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCircleDetailQuery(string Name) : IRequest<CircleResponse>;

//public record CircleResponse
//{
//	public int Id { get; set; }
//	public string Name { get; set; }
//	public string Status { get; set; }

//	public required IEnumerable<string> ArrangementSongTitles { get; set; }

//	public CircleResponse(int id, string name, string status)
//		=> (Id, Name, Status) = (id, name, status);
//}

class GetCircleDetailQueryHandler : BaseHandler<GetCircleDetailQuery, CircleResponse>
{
	public GetCircleDetailQueryHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

	public override async Task<CircleResponse> Handle(GetCircleDetailQuery query, CancellationToken cancellationToken)
	{
		var circle = await _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => c.Name == query.Name)
			.Select(c => new CircleResponse(c.Id, c.Name, c.Status.ToString())
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title),
			})
			.SingleOrDefaultAsync();

		if (circle is null)
		{
			throw new AppException(HttpStatusCode.NotFound, $"Circle {query.Name} not found.");
		}

		return circle;
	}
}
