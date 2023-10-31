using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Official.OfficialSongs
{
	public record CreateOfficialSongCommand(string Title, string Origin) : IRequest;

	class CreateOfficialSongCommandHandler : IRequestHandler<CreateOfficialSongCommand>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateOfficialSongCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
		{
			var song = new OfficialSong
			{
				Title = command.Title,
				Origin = command.Origin,
			};

			_context.OfficialSongs.Add(song);
			await _context.SaveChangesAsync();

			return;
		}
	}
}
