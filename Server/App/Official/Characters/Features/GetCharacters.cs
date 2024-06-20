using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.Characters.Features;

public record GetCharactersQuery(string? searchName) : IRequest<Result<IEnumerable<CharacterResponse>>>;

public record CharacterResponse : BaseAuditedEntityResponse
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required string OriginGameCode { get; set; }
	public required List<string> OfficialSongTitles { get; set; }

	public CharacterResponse(Character character) : base(character)
		=> (Name, ImageUrl) = (character.Name, character.ImageUrl);
}

class GetCharactersHandler : BaseHandler<GetCharactersQuery, IEnumerable<CharacterResponse>>
{
	public GetCharactersHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<CharacterResponse>>> Handle(GetCharactersQuery query, CancellationToken cancellationToken)
	{
		var characters_Res = await _context.Characters
			.Include(c => c.OriginGame)
			.Include(c => c.OfficialSongs)
			.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
			.OrderBy(c => c.OriginGame.ReleaseDate)
			.Select(c => new CharacterResponse(c)
			{
				OriginGameCode = c.OriginGame.GameCode,
				OfficialSongTitles = c.OfficialSongs.Select(os => os.Title).ToList(),
			})
			.ToListAsync();

		return _resultFactory.Ok(characters_Res);
	}
}
