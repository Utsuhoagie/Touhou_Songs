using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.App.Official.OfficialGames.Features
{
	public record CreateOfficialGameCommand : IRequest
	{
		public string Title { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; } = string.Empty;

		public CreateOfficialGameCommand(string title, DateTime releaseDate, string imageUrl)
			=> (Title, ReleaseDate, ImageUrl) = (title, releaseDate, imageUrl);
	}

	class CreateOfficialGameCommandHandler : IRequestHandler<CreateOfficialGameCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialGameCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialGameCommand command, CancellationToken cancellationToken)
		{
			var officialGame = new OfficialGame(command.Title, command.ReleaseDate, command.ImageUrl);

			_context.OfficialGames.Add(officialGame);
			await _context.SaveChangesAsync();
		}
	}
}
