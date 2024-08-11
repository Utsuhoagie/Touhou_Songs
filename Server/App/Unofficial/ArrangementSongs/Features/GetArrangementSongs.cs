﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.App.Unofficial.Songs;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntity;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record GetArrangementSongsQuery() : IRequest<Result<IEnumerable<ArrangementSongResponse>>>;

public record ArrangementSongResponse : BaseAuditedEntityResponse
{
	public string Title { get; set; }
	public string Url { get; set; }
	public UnofficialStatus Status { get; set; }

	public required string CircleName { get; set; }
	public required List<string> OfficialSongTitles { get; set; }

	public ArrangementSongResponse(ArrangementSong arrangementSong) : base(arrangementSong)
		=> (Title, Url, Status) = (arrangementSong.Title, arrangementSong.Url, arrangementSong.Status);
}

class GetArrangementSongsHandler : BaseHandler<GetArrangementSongsQuery, IEnumerable<ArrangementSongResponse>>
{
	public GetArrangementSongsHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<ArrangementSongResponse>>> Handle(GetArrangementSongsQuery request, CancellationToken cancellationToken)
	{
		//var entity = await _context.Set<BaseEntity>()
		//	.Where(b => b.Id == 5)
		//	.ToListAsync();

		//var foo = await _context.ArrangementSongs
		//	//.Include(a => a.Circle)
		//	.ToListAsync();

		var arrangementSongs_Res = await _context.ArrangementSongs
			//.Include(a => a.Circle)
			//.Include(a => a.OfficialSongs)
			.Select(a => new ArrangementSongResponse(a)
			{
				CircleName = "asdsa", //a.Circle.Name,
				OfficialSongTitles = a.OfficialSongs
					.Select(os => os.Game)
					.Select(og => og.Title)
					.ToList(),
			})
			.ToListAsync();

		return _resultFactory.Ok(arrangementSongs_Res);
	}
}
