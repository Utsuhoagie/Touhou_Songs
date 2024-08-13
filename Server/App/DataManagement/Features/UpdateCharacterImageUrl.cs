using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record UpdateCharacterImageUrlsCommand : IRequest<Result<string>>
{
	public List<UpdateCharacterImageUrl> UpdateCharacterImageUrls { get; set; } = new();
	public record UpdateCharacterImageUrl
	{
		public string Name { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string GameCode { get; set; } = string.Empty;
		public List<string> Songs { get; set; } = new();
	}
}

class UpdateCharacterImageUrlsHandler : BaseHandler<UpdateCharacterImageUrlsCommand, string>
{
	public UpdateCharacterImageUrlsHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<string>> Handle(UpdateCharacterImageUrlsCommand command, CancellationToken cancellationToken)
	{
		var updateCharacters = command.UpdateCharacterImageUrls;

		var characterNames = updateCharacters.Select(u => u.Name);
		var charactersToUpdate = await _context.Characters
			.Include(c => c.OfficialSongs)
			.Where(c => characterNames.Contains(c.Name))
			.ToListAsync();

		foreach (var character in charactersToUpdate)
		{
			var updatedCharacter = updateCharacters.SingleOrDefault(u => u.Name == character.Name);

			character.ImageUrl = updatedCharacter!.ImageUrl;
		}

		await _context.SaveChangesAsync();

		return _resultFactory.Ok("Image Urls updated");
	}
}