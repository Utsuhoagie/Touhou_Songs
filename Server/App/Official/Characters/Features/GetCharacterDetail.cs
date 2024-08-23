using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.App.Official.OfficialSongs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.Characters.Features;

public record GetCharacterDetailQuery(string Name) : IRequest<Result<CharacterDetailResponse>>;

public record CharacterDetailResponse : BaseAuditedEntityResponse
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required OfficialGameSimple OriginGame { get; set; }
	public record OfficialGameSimple : BaseAuditedEntityResponse
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string NumberCode { get; set; }
		public string ImageUrl { get; set; }

		public OfficialGameSimple(OfficialGame officialGame) : base(officialGame)
			=> (Title, GameCode, NumberCode, ImageUrl)
			= (officialGame.Title, officialGame.GameCode, officialGame.NumberCode, officialGame.ImageUrl);
	}

	public required List<OfficialSongSimple> OfficialSongs { get; set; }
	public record OfficialSongSimple : BaseAuditedEntityResponse
	{
		public string Title { get; set; }
		public string Context { get; set; }

		public OfficialSongSimple(OfficialSong officialSong) : base(officialSong)
			=> (Title, Context) = (officialSong.Title, officialSong.Context);
	}

	public CharacterDetailResponse(Character character) : base(character)
		=> (Name, ImageUrl) = (character.Name, character.ImageUrl);
}

class GetCharacterDetailHandler : BaseHandler<GetCharacterDetailQuery, CharacterDetailResponse>
{
	public GetCharacterDetailHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<CharacterDetailResponse>> Handle(GetCharacterDetailQuery query, CancellationToken cancellationToken)
	{
		var character_Res = await _context.Characters
			.Include(c => c.OriginGame)
			.Include(c => c.OfficialSongs)
			.Where(c => c.Name == query.Name)
			.Select(c => new CharacterDetailResponse(c)
			{
				OriginGame = new(c.OriginGame),
				OfficialSongs = c.OfficialSongs
					.Select(os => new CharacterDetailResponse.OfficialSongSimple(os))
					.ToList(),
			})
			.SingleOrDefaultAsync();

		if (character_Res is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(Character), query.Name));
		}

		return _resultFactory.Ok(character_Res);
	}
}
