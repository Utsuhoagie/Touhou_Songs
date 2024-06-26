﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.Unofficial.ArrangementSongs.Features;

public record GetArrangementSongsQuery() : IRequest<Result<IEnumerable<ArrangementSongResponse>>>;

public record ArrangementSongResponse
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Url { get; set; }
	public string Status { get; set; }

	public required string CircleName { get; set; }
	public required IEnumerable<string> OfficialSongTitles { get; set; }

	public ArrangementSongResponse(int id, string title, string url, string status)
		=> (Id, Title, Url, Status) = (id, title, url, status);
}

class GetArrangementSongsHandler : BaseHandler<GetArrangementSongsQuery, IEnumerable<ArrangementSongResponse>>
{
	public GetArrangementSongsHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<IEnumerable<ArrangementSongResponse>>> Handle(GetArrangementSongsQuery request, CancellationToken cancellationToken)
	{
		var arrangementSongs_Res = await _context.ArrangementSongs
			.Include(a => a.Circle)
			.Include(a => a.OfficialSongs)
			.Select(a => new ArrangementSongResponse(a.Id, a.Title, a.Url, a.Status.ToString())
			{
				CircleName = a.Circle.Name,
				OfficialSongTitles = a.OfficialSongs.Select(os => os.Title),
			})
			.ToListAsync();

		return _resultFactory.Ok(arrangementSongs_Res);
	}
}
