using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.App.Unofficial.Circles.Features
{
	public record GetCirclesQuery(string? searchName) : IRequest<IEnumerable<CircleResponse>>;

	public record CircleResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }

		public required List<string> ArrangementSongs { get; set; }

		public CircleResponse(int id, string name, string status)
			=> (Id, Name, Status) = (id, name, status);
	}

	public class GetCirclesQueryHandler : IRequestHandler<GetCirclesQuery, IEnumerable<CircleResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetCirclesQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<CircleResponse>> Handle(GetCirclesQuery query, CancellationToken cancellationToken)
		{
			var circles = await _context.Circles
				.Include(c => c.ArrangementSongs)
				.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
				.Select(c => new CircleResponse(c.Id, c.Name, c.Status.ToString())
				{
					ArrangementSongs = c.ArrangementSongs.Select(a => a.Title).ToList(),
				})
				.ToListAsync();

			return circles;
		}
	}
}
