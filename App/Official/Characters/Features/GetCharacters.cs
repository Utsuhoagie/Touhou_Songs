using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;

namespace Touhou_Songs.App.Official.Characters.Features
{
	public record GetCharactersQuery(string? searchName) : IRequest<IEnumerable<CharacterResponse>>;

	public record CharacterResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		public string OriginGameCode { get; set; }
		public required IEnumerable<string> SongTitles { get; set; }

		public CharacterResponse(int id, string name, string imageUrl, string originGameCode)
			=> (Id, Name, ImageUrl, OriginGameCode) = (id, name, imageUrl, originGameCode);
	}

	class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, IEnumerable<CharacterResponse>>
	{
		private readonly Touhou_Songs_Context _context;

		public GetCharactersQueryHandler(Touhou_Songs_Context context) => _context = context;

		public async Task<IEnumerable<CharacterResponse>> Handle(GetCharactersQuery query, CancellationToken cancellationToken)
		{
			var characterResponses = await _context.Characters
				.Include(c => c.OriginGame)
				.Include(c => c.Songs)
				.Where(c => query.searchName == null || EF.Functions.ILike(c.Name, $"%{query.searchName}%"))
				.Select(c => new CharacterResponse(c.Id, c.Name, c.ImageUrl, c.OriginGame.GameCode)
				{
					SongTitles = c.Songs.Select(os => os.Title),
				})
				.ToListAsync();

			return characterResponses;
		}
	}
}
