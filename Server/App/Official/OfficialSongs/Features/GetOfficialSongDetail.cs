﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Official.OfficialSongs.Features;

public record GetOfficialSongDetailQuery(int Id) : IRequest<Result<OfficialSongDetailResponse>>;

public record OfficialSongDetailResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Context { get; set; }

	public required OfficialGameSimple Game { get; set; }
	public record OfficialGameSimple
	{
		public string Title { get; set; }
		public string GameCode { get; set; }
		public string ImageUrl { get; set; }

		public OfficialGameSimple(string title, string gameCode, string imageUrl)
			=> (Title, GameCode, ImageUrl) = (title, gameCode, imageUrl);
	}

	public OfficialSongDetailResponse(int id, string title, string context)
		=> (Id, Title, Context) = (id, title, context);
}

class GetOfficialSongDetailHandler : BaseHandler<GetOfficialSongDetailQuery, OfficialSongDetailResponse>
{
	public GetOfficialSongDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<OfficialSongDetailResponse>> Handle(GetOfficialSongDetailQuery query, CancellationToken cancellationToken)
	{
		var officialSongDetail_Res = await _context.OfficialSongs
			.Include(os => os.Game)
			.Where(os => os.Id == query.Id)
			.Select(os => new OfficialSongDetailResponse(os.Id, os.Title, os.Context)
			{
				Game = new OfficialSongDetailResponse.OfficialGameSimple(os.Game.Title, os.Game.GameCode, os.Game.ImageUrl),
			})
			.SingleOrDefaultAsync();

		if (officialSongDetail_Res is null)
		{
			return _resultFactory.NotFound($"OfficialSong {query.Id} not found.");
		}

		return _resultFactory.Ok(officialSongDetail_Res);
	}
}
