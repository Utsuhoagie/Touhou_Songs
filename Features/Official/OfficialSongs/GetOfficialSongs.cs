using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Official.OfficialSongs
{
	public record GetOfficialSongsQuery() : IRequest<IEnumerable<OfficialSongResponse>>;

	public record OfficialSongResponse(int Id, string Title, string Context);

	class GetOfficialSongsQueryHandler : IRequestHandler<GetOfficialSongsQuery, IEnumerable<OfficialSongResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetOfficialSongsQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<OfficialSongResponse>> Handle(GetOfficialSongsQuery query, CancellationToken cancellationToken)
		{
			var officialSongResponses = await _context.OfficialSongs
				.Select(s => new OfficialSongResponse(s.Id, s.Title, s.Context))
				.ToListAsync();

			return officialSongResponses;
		}
	}
}
