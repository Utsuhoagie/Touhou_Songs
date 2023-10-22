using MediatR;
using Touhou_Songs.Data;

namespace Touhou_Songs.Features.Songs
{
	public record CreateSongRequest(string Title, string Origin) : IRequest;

	class CreateSongCommandHandler : IRequestHandler<CreateSongRequest>
	{
		private readonly Touhou_Songs_Context _context;

		public CreateSongCommandHandler(Touhou_Songs_Context context) => _context = context;

		public async Task Handle(CreateSongRequest command, CancellationToken cancellationToken)
		{
			var song = new Song
			{
				Title = command.Title,
				Origin = command.Origin,
			};

			_context.Songs.Add(song);
			await _context.SaveChangesAsync();

			return;
		}
	}
}
