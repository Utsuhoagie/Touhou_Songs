using MediatR;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialGames.Features;

public record CreateOfficialGameCommand(string Title, string GameCode, string NumberCode, DateTime ReleaseDate, string ImageUrl) : IRequest<Result<string>>;

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
