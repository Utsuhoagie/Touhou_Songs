using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Official.OfficialGames
{
	public record GetOfficialGamesQuery() : IRequest<IEnumerable<OfficialGameResponse>>;

	public record OfficialGameResponse(int Id, string Title, DateTime ReleaseDate, string ImageUrl);

	class GetOfficialGamesQueryHandler : IRequestHandler<GetOfficialGamesQuery, IEnumerable<OfficialGameResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetOfficialGamesQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<OfficialGameResponse>> Handle(GetOfficialGamesQuery query, CancellationToken cancellationToken)
		{
			var officialGamesResponse = await _context.OfficialGames
				.Select(og => new OfficialGameResponse(og.Id, og.Title, og.ReleaseDate, og.ImageUrl))
				.ToListAsync();

			return officialGamesResponse;
		}
	}
}
