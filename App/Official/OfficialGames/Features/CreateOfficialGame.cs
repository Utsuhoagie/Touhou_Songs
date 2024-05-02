using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record CreateOfficialGameCommand : IRequest<Result<string>>
{
	public string Title { get; set; }
	public string GameCode { get; set; }
	public string NumberCode { get; set; }
	public DateTime ReleaseDate { get; set; }
	public string ImageUrl { get; set; }

	public CreateOfficialGameCommand(string title, string gameCode, string numberCode, DateTime releaseDate, string imageUrl)
		=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl) = (title, gameCode, numberCode, releaseDate, imageUrl);
}

class CreateOfficialGameHandler : BaseHandler<CreateOfficialGameCommand, string>
{
	public CreateOfficialGameHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<string>> Handle(CreateOfficialGameCommand command, CancellationToken cancellationToken)
	{
		var officialGame = new OfficialGame(command.Title, command.GameCode, command.NumberCode, command.ReleaseDate, command.ImageUrl)
		{
			Songs = new(),
		};

		_context.OfficialGames.Add(officialGame);
		await _context.SaveChangesAsync();

		return _resultFactory.Ok(officialGame.Title);
	}
}
