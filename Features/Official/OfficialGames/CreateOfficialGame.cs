using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Official.OfficialGames
{
	public record CreateOfficialGameCommand(int Id, string Title, DateTime ReleaseDate, string ImageUrl) : IRequest;

	class CreateOfficialGameCommandHandler : IRequestHandler<CreateOfficialGameCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialGameCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialGameCommand command, CancellationToken cancellationToken)
		{
			var officialGame = new OfficialGame
			{
				Id = command.Id,
				Title = command.Title,
				ReleaseDate = command.ReleaseDate,
				ImageUrl = command.ImageUrl,
			};

			_context.OfficialGames.Add(officialGame);
			await _context.SaveChangesAsync();
		}
	}
}
