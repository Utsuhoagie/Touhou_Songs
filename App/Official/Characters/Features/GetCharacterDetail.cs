using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.Characters.Features;

public record GetCharacterDetailQuery(string Name) : IRequest<Result<CharacterDetailResponse>>;

public record CharacterDetailResponse
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required OfficialGameSimple OriginGame { get; set; }
	public record OfficialGameSimple
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string NumberCode { get; set; }
		public string ImageUrl { get; set; }

		public OfficialGameSimple(string title, string gameCode, string numberCode, string imageUrl)
			=> (Title, GameCode, NumberCode, ImageUrl) = (title, gameCode, numberCode, imageUrl);
	}

	public required IEnumerable<OfficialSongSimple> OfficialSongs { get; set; }
	public record OfficialSongSimple
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }

		public OfficialSongSimple(int id, string title, string context) => (Id, Title, Context) = (id, title, context);
	}

	public CharacterDetailResponse(int id, string name, string imageUrl)
		=> (Id, Name, ImageUrl) = (id, name, imageUrl);
}

class GetCharacterDetailHandler : BaseHandler<GetCharacterDetailQuery, CharacterDetailResponse, Result<CharacterDetailResponse>>
{
	public GetCharacterDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<CharacterDetailResponse>> Handle(GetCharacterDetailQuery request, CancellationToken cancellationToken)
	{
		var character_Res = await _context.Characters
			.Include(c => c.OriginGame)
			.Include(c => c.OfficialSongs)
			.Where(c => c.Name == request.Name)
			.Select(c => new CharacterDetailResponse(c.Id, c.Name, c.ImageUrl)
			{
				OriginGame = new(c.OriginGame.Title, c.OriginGame.GameCode, c.OriginGame.NumberCode, c.OriginGame.ImageUrl),
				OfficialSongs = c.OfficialSongs.Select(os => new CharacterDetailResponse.OfficialSongSimple(os.Id, os.Title, os.Context)).ToList(),
			})
			.SingleOrDefaultAsync();

		if (character_Res is null)
		{
			return NotFound($"Character {request.Name} not found.");
		}

		return Ok(character_Res);
	}
}
