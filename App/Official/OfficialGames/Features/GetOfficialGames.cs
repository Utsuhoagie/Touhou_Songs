using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.OfficialGames.Features
{
	public record GetOfficialGamesQuery(string? searchTitle) : IRequest<IEnumerable<OfficialGameResponse>>;

	public record OfficialGameResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string NumberCode { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; }

		public required IEnumerable<string> SongTitles { get; set; }

		public OfficialGameResponse(int id, string title, string gameCode, string numberCode, DateTime releaseDate, string imageUrl)
			=> (Id, Title, GameCode, NumberCode, ReleaseDate, ImageUrl) = (id, title, gameCode, numberCode, releaseDate, imageUrl);
	}

	class GetOfficialGamesQueryHandler : BaseHandler<GetOfficialGamesQuery, IEnumerable<OfficialGameResponse>>
	{
		public GetOfficialGamesQueryHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

		public override async Task<IEnumerable<OfficialGameResponse>> Handle(GetOfficialGamesQuery query, CancellationToken cancellationToken)
		{
			var officialGamesResponse = await _context.OfficialGames
				.Include(og => og.Songs)
				.Where(og => query.searchTitle == null || EF.Functions.ILike(og.Title, $"%{query.searchTitle}%"))
				.OrderBy(og => og.ReleaseDate)
				.Select(og => new OfficialGameResponse(og.Id, og.Title, og.GameCode, og.NumberCode, og.ReleaseDate, og.ImageUrl)
				{
					SongTitles = og.Songs
						.OrderBy(os => os.Id)
						.Select(os => os.Title),
				})
				.ToListAsync();

			return officialGamesResponse;
		}
	}
}
