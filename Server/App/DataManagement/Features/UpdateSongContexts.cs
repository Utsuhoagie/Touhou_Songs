using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record UpdateSongContextsCommand : IRequest<Result<string>>
{
	public List<UpdateSongContext> UpdateSongContexts { get; set; } = new();
	public record UpdateSongContext
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string GameCode { get; set; } = string.Empty;

		/// <summary>
		/// NOTE: This is NEW context!
		/// </summary>
		public string Context { get; set; } = string.Empty;
	}
}

class UpdateSongsContextHandler : BaseHandler<UpdateSongContextsCommand, string>
{
	public UpdateSongsContextHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<string>> Handle(UpdateSongContextsCommand command, CancellationToken cancellationToken)
	{
		var updateSongContexts = command.UpdateSongContexts;

		var songIds = updateSongContexts.Select(u => u.Id).ToList();

		var songsToUpdate = await _context.OfficialSongs
			.Where(os => songIds.Contains(os.Id))
			.ToListAsync();

		foreach (var song in songsToUpdate)
		{
			var updatedSong = updateSongContexts.SingleOrDefault(u => u.Id == song.Id);

			song.Context = updatedSong!.Context;
		}

		await _context.SaveChangesAsync();

		return _resultFactory.Ok("Song contexts updated");
	}
}