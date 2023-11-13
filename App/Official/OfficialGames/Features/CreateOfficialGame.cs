using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.App.Official.OfficialGames.Features
{
	public record CreateOfficialGameCommand : IRequest
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string NumberCode { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; }

		public CreateOfficialGameCommand(string title, string gameCode, string numberCode, DateTime releaseDate, string imageUrl)
			=> (Title, GameCode, NumberCode, ReleaseDate, ImageUrl) = (title, gameCode, numberCode, releaseDate, imageUrl);
	}

	class CreateOfficialGameCommandHandler : IRequestHandler<CreateOfficialGameCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialGameCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialGameCommand command, CancellationToken cancellationToken)
		{
			var officialGame = new OfficialGame(command.Title, command.GameCode, command.NumberCode, command.ReleaseDate, command.ImageUrl);

			_context.OfficialGames.Add(officialGame);
			await _context.SaveChangesAsync();
		}
	}
}
