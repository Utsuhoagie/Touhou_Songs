using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Official.OfficialSongs.Features
{
	public record CreateOfficialSongCommand : IRequest<string>
	{
		public string Title { get; set; }
		public string Context { get; set; }

		public string GameCode { get; set; }
		public required List<string> CharacterNames { get; set; }

		public CreateOfficialSongCommand(string title, string context, string gameCode)
			=> (Title, Context, GameCode) = (title, context, gameCode);
	}

	class CreateOfficialSongCommandHandler : BaseHandler<CreateOfficialSongCommand, string>
	{
		public CreateOfficialSongCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

		public override async Task<string> Handle(CreateOfficialSongCommand command, CancellationToken cancellationToken)
		{
			var officialGame = await _context.OfficialGames
				.SingleOrDefaultAsync(og => EF.Functions.ILike(og.GameCode, $"{command.GameCode}"));

			if (officialGame is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Official Game {command.GameCode} not found");
			}

			var charactersWithSong = await _context.Characters
				.Where(c => command.CharacterNames.Contains(c.Name))
				.ToListAsync();

			var officialSong = new OfficialSong(command.Title, command.Context)
			{
				GameId = officialGame.Id,
				Game = officialGame,
				Characters = charactersWithSong,
				ArrangementSongs = new(),
				OfficialSongArrangementSongs = new(),
			};

			_context.OfficialSongs.Add(officialSong);
			await _context.SaveChangesAsync();

			return officialSong.Title;
		}
	}
}
