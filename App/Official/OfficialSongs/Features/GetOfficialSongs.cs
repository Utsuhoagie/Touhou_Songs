﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record GetOfficialSongsQuery(string? SearchTitle, string? GameCode) : IRequest<IEnumerable<OfficialSongResponse>>;

public record OfficialSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Context { get; set; }

	public string GameCode { get; set; }

	public OfficialSongResponse(int id, string title, string context, string gameCode)
		=> (Id, Title, Context, GameCode) = (id, title, context, gameCode);
}

class GetOfficialSongsHandler : BaseHandler<GetOfficialSongsQuery, IEnumerable<OfficialSongResponse>>
{
	public GetOfficialSongsHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

	public override async Task<IEnumerable<OfficialSongResponse>> Handle(GetOfficialSongsQuery query, CancellationToken cancellationToken)
	{
		var officialSongResponses = await _context.OfficialSongs
			.Include(os => os.Game)
			.Where(os => query.SearchTitle == null || EF.Functions.ILike(os.Title, $"%{query.SearchTitle}%"))
			.Where(os => query.GameCode == null || os.Game.GameCode == query.GameCode)
			.OrderBy(os => os.GameId).ThenBy(os => os.Id)
			.Select(os => new OfficialSongResponse(os.Id, os.Title, os.Context, os.Game.GameCode))
			.ToListAsync();

		return officialSongResponses;
	}
}
