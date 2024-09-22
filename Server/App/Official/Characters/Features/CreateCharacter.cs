using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.i18n;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.Characters.Features;

public record CreateCharacterCommand(string Name, string ImageUrl, string OriginGameCode, List<string> SongTitles) : IRequest<Result<string>>;

class CreateCharacterHandler : BaseHandler<CreateCharacterCommand, string>
{
	public CreateCharacterHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
	{
		var dbOriginGame = await _context.OfficialGames
			.SingleOrDefaultAsync(og => EF.Functions.Like(og.GameCode, $"{command.OriginGameCode}"));

		if (dbOriginGame is null)
		{
			return _resultFactory.NotFound(GenericI18n.NotFound.ToLanguage(Lang.EN, "Official OriginGame", command.OriginGameCode));
		}

		var dbOfficialSongs = await _context.OfficialSongs
			.Where(os => command.SongTitles.Contains(os.Title))
			.ToListAsync();

		var character = new Character(command.Name, command.ImageUrl)
		{
			OriginGameId = dbOriginGame.Id,
			OriginGame = dbOriginGame,
			OfficialSongs = dbOfficialSongs,
		};

		var createdCharacter = _context.Characters.Add(character).Entity;
		await _context.SaveChangesAsync();

		return _resultFactory.Ok(createdCharacter.Name);
	}
}