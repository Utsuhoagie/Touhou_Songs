using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Official.Characters.Features
{
	public record GetCharacterDetailQuery(string Name) : IRequest<CharacterDetailResponse>;

	public record OfficialGameSimple
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string NumberCode { get; set; }
		public string ImageUrl { get; set; }

		public OfficialGameSimple(string title, string gameCode, string numberCode, string imageUrl)
			=> (Title, GameCode, NumberCode, ImageUrl) = (title, gameCode, numberCode, imageUrl);
	}

	public record OfficialSongSimple
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Context { get; set; }

		public OfficialSongSimple(int id, string title, string context) => (Id, Title, Context) = (id, title, context);
	}

	public record CharacterDetailResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public required OfficialGameSimple OriginGame { get; set; }
		public required IEnumerable<OfficialSongSimple> OfficialSongs { get; set; }

		public CharacterDetailResponse(int id, string name, string imageUrl)
			=> (Id, Name, ImageUrl) = (id, name, imageUrl);
	}

	class GetCharacterDetailQueryHandler : BaseHandler<GetCharacterDetailQuery, CharacterDetailResponse>
	{
		public GetCharacterDetailQueryHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

		public override async Task<CharacterDetailResponse> Handle(GetCharacterDetailQuery request, CancellationToken cancellationToken)
		{
			var character = await _context.Characters
				.Include(c => c.OriginGame)
				.Include(c => c.OfficialSongs)
				.Where(c => c.Name == request.Name)
				.Select(c => new CharacterDetailResponse(c.Id, c.Name, c.ImageUrl)
				{
					OriginGame = new OfficialGameSimple(c.OriginGame.Title, c.OriginGame.GameCode, c.OriginGame.NumberCode, c.OriginGame.ImageUrl),
					OfficialSongs = c.OfficialSongs.Select(os => new OfficialSongSimple(os.Id, os.Title, os.Context)).ToList(),
				})
				.SingleOrDefaultAsync();

			if (character is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Character {request.Name} not found.");
			}

			return character;
		}
	}
}
