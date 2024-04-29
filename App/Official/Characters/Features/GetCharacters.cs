using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.Characters.Features;

public record GetCharactersQuery(string? searchName) : IRequest<IEnumerable<CharacterResponse>>;

public record CharacterResponse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required string OriginGameCode { get; set; }
	public required IEnumerable<string> OfficialSongTitles { get; set; }

	public CharacterResponse(int id, string name, string imageUrl)
		=> (Id, Name, ImageUrl) = (id, name, imageUrl);
}

class GetCharactersHandler : BaseHandler<GetCharactersQuery, IEnumerable<CharacterResponse>>
{
	public GetCharactersHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

	public override async Task<IEnumerable<CharacterResponse>> Handle(GetCharactersQuery query, CancellationToken cancellationToken)
	{
		var characterResponses = await _context.Characters
			.Include(c => c.OriginGame)
			.Include(c => c.OfficialSongs)
			.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
			.OrderBy(c => c.OriginGame.ReleaseDate)
			.Select(c => new CharacterResponse(c.Id, c.Name, c.ImageUrl)
			{
				OriginGameCode = c.OriginGame.GameCode,
				OfficialSongTitles = c.OfficialSongs.Select(os => os.Title),
			})
			.ToListAsync();

		return characterResponses;
	}
}
