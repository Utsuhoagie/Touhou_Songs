using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Songs
{
	public record GetSongsQuery() : IRequest<IEnumerable<SongResponse>>;

	public record SongResponse(int Id, string Title, string Origin);

	class GetSongsQueryHandler : IRequestHandler<GetSongsQuery, IEnumerable<SongResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetSongsQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<SongResponse>> Handle(GetSongsQuery query, CancellationToken cancellationToken)
		{
			var songs = await _context.Songs
				.Select(s => new SongResponse(s.Id, s.Title, s.Origin))
				.ToListAsync();

			return songs;
		}
	}
}
