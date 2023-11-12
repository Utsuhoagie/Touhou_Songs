using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.App.Official.OfficialSongs.Features
{
	public record GetOfficialSongsQuery(string? searchTitle) : IRequest<IEnumerable<OfficialSongResponse>>;

	public record OfficialSongResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }

		public string GameCode { get; set; }

		public OfficialSongResponse(int id, string title, string context, string gameCode)
			=> (Id, Title, Context, GameCode) = (id, title, context, gameCode);
	}

	class GetOfficialSongsQueryHandler : IRequestHandler<GetOfficialSongsQuery, IEnumerable<OfficialSongResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetOfficialSongsQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<OfficialSongResponse>> Handle(GetOfficialSongsQuery query, CancellationToken cancellationToken)
		{
			var officialSongResponses = await _context.OfficialSongs
				.Include(os => os.Game)
				.Where(os => query.searchTitle == null || EF.Functions.ILike(os.Title, $"%{query.searchTitle}%"))
				.OrderBy(os => os.GameId).ThenBy(os => os.Id)
				.Select(os => new OfficialSongResponse(os.Id, os.Title, os.Context, os.Game.GameCode))
				.ToListAsync();

			return officialSongResponses;
		}
	}
}
