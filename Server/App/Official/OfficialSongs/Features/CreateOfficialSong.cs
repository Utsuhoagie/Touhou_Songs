using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Official.OfficialGames;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record CreateOfficialSongCommand(string Title, string Context, string GameCode, List<string> CharacterNames) : IRequest<Result<string>>;

class CreateOfficialSongHandler : BaseHandler<CreateOfficialSongCommand, string>
{
	public CreateOfficialSongHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
	{
		var dbOfficialGame = await _context.OfficialGames
			.SingleOrDefaultAsync(og => EF.Functions.Like(og.GameCode, $"{command.GameCode}"));

		if (dbOfficialGame is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, nameof(OfficialGame), command.GameCode));
		}

		var dbCharactersWithSong = await _context.Characters
			.Where(c => command.CharacterNames.Contains(c.Name))
			.ToListAsync();

		var officialSong = new OfficialSong(command.Title, command.Context)
		{
			GameId = dbOfficialGame.Id,
			Game = dbOfficialGame,
			Characters = dbCharactersWithSong,
			CharacterOfficialSongs = new(),
			ArrangementSongs = new(),
			OfficialSongArrangementSongs = new(),
		};

		var createdOfficialSong = _context.OfficialSongs.Add(officialSong).Entity;
		await _context.SaveChangesAsync();

		return _resultFactory.Ok(createdOfficialSong.Title);
	}
}
