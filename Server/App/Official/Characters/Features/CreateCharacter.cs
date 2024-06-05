using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.Characters.Features;

public record CreateCharacterCommand : IRequest<Result<string>>
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public string OriginGameCode { get; set; }
	public required List<string> SongTitles { get; set; }

	public CreateCharacterCommand(string name, string imageUrl, string originGameCode)
		=> (Name, ImageUrl, OriginGameCode) = (name, imageUrl, originGameCode);
}

class CreateCharacterHandler : BaseHandler<CreateCharacterCommand, string>
{
	public CreateCharacterHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
	{
		var dbOriginGame = await _context.OfficialGames
			.SingleOrDefaultAsync(og => EF.Functions.ILike(og.GameCode, $"{command.OriginGameCode}"));

		if (dbOriginGame is null)
		{
			return _resultFactory.NotFound($"Official OriginGame {command.OriginGameCode} not found");
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

		_context.Characters.Add(character);
		await _context.SaveChangesAsync();

		return _resultFactory.Ok(character.Name);
	}
}