using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.Characters;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record ImportCharactersCommand : IRequest<Result<string>>
{
	public List<ImportCharacter> ImportCharacters { get; set; } = new();
	public record ImportCharacter
	{
		public string Name { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;

		public string GameCode { get; set; } = string.Empty;
		public List<string> Songs { get; set; } = new();
	}
}

class ImportCharactersHandler : BaseHandler<ImportCharactersCommand, string>
{
	public ImportCharactersHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<string>> Handle(ImportCharactersCommand command, CancellationToken cancellationToken)
	{
		var importCharacters = command.ImportCharacters;

		var importGameCodes = importCharacters.Select(ic => ic.GameCode).ToList();

		var games = await _context.OfficialGames
			.Where(og => importGameCodes.Contains(og.GameCode))
			.ToListAsync();

		var importSongsByCharacter = importCharacters
			.Select(ic => new
			{
				Character = ic.Name,
				ic.Songs,
			})
			.ToList();

		var importSongTitles = importSongsByCharacter
			.SelectMany(isbc => isbc.Songs)
			.ToList();

		var songs = await _context.OfficialSongs
			.Where(os => importSongTitles.Contains(os.Title))
			.ToListAsync();

		var newCharacters = new List<Character>();

		foreach (var importCharacter in importCharacters)
		{
			var originGame = games.SingleOrDefault(og => og.GameCode == importCharacter.GameCode);

			if (originGame is null)
			{
				continue;
			}

			var characterSongTitles = importSongsByCharacter
				.SingleOrDefault(isbc => isbc.Character == importCharacter.Name)
				!
				.Songs;

			var characterSongs = songs
				.Where(os => characterSongTitles.Contains(os.Title))
				.ToList();

			var character = new Character(importCharacter.Name, importCharacter.ImageUrl)
			{
				OriginGameId = originGame.Id,
				OriginGame = originGame,
				OfficialSongs = characterSongs,
			};

			newCharacters.Add(character);
		}

		_context.Characters.AddRange(newCharacters);
		await _context.SaveChangesAsync();

		return _resultFactory.Ok("Characters imported");
	}
}