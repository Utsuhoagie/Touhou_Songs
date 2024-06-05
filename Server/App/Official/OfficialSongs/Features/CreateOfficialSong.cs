using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record CreateOfficialSongCommand : IRequest<Result<string>>
{
	public string Title { get; set; }
	public string Context { get; set; }

	public required string GameCode { get; set; }
	public required List<string> CharacterNames { get; set; }

	public CreateOfficialSongCommand(string title, string context)
		=> (Title, Context) = (title, context);
}

class CreateOfficialSongHandler : BaseHandler<CreateOfficialSongCommand, string>
{
	public CreateOfficialSongHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
	{
		var dbOfficialGame = await _context.OfficialGames
			.SingleOrDefaultAsync(og => EF.Functions.ILike(og.GameCode, $"{command.GameCode}"));

		if (dbOfficialGame is null)
		{
			return _resultFactory.NotFound($"Official Game {command.GameCode} not found");
		}

		var dbCharactersWithSong = await _context.Characters
			.Where(c => command.CharacterNames.Contains(c.Name))
			.ToListAsync();

		var officialSong = new OfficialSong(command.Title, command.Context)
		{
			GameId = dbOfficialGame.Id,
			Game = dbOfficialGame,
			Characters = dbCharactersWithSong,
			ArrangementSongs = new(),
			OfficialSongArrangementSongs = new(),
		};

		_context.OfficialSongs.Add(officialSong);
		await _context.SaveChangesAsync();

		return _resultFactory.Ok(officialSong.Title);
	}
}
