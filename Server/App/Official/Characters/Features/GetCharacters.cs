using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Paging;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.Characters.Features;

public record GetCharactersQuery(string? SearchName) : PagingParams, IRequest<Result<Paged<CharacterResponse>>>;

public record CharacterResponse : BaseAuditedEntityResponse
{
	public string Name { get; set; }
	public string ImageUrl { get; set; }

	public required string OriginGameCode { get; set; }
	public required List<string> OfficialSongTitles { get; set; }

	public CharacterResponse(Character character) : base(character)
		=> (Name, ImageUrl) = (character.Name, character.ImageUrl);
}

class GetCharactersHandler : BaseHandler<GetCharactersQuery, Paged<CharacterResponse>>
{
	public GetCharactersHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<Paged<CharacterResponse>>> Handle(GetCharactersQuery query, CancellationToken cancellationToken)
	{
		var getCharactersQuery = _context.Characters
			.Include(c => c.OriginGame)
			.Include(c => c.OfficialSongs)
			.Where(c => query.SearchName == null || EF.Functions.Like(c.Name, $"%{query.SearchName}%"))
			.OrderBy(c => c.OriginGame.ReleaseDate);

		var characters_Res = await getCharactersQuery
			.Skip((query.Page - 1) * query.PageSize)
			.Take(query.PageSize)
			.Select(c => new CharacterResponse(c)
			{
				OriginGameCode = c.OriginGame.GameCode,
				OfficialSongTitles = c.OfficialSongs.Select(os => os.Title).ToList(),
			})
			.ToListAsync();

		var totalCharacters = await getCharactersQuery.CountAsync();

		var pagedCharacters_Res = new Paged<CharacterResponse>(query)
		{
			TotalItemsCount = totalCharacters,
			Items = characters_Res,
		};

		return _resultFactory.Ok(pagedCharacters_Res);
	}
}
