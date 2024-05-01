﻿using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Unofficial.Circles.Features;

public record GetCircleDetailQuery(string Name) : IRequest<Result<CircleResponse>>;

class GetCircleDetailHandler : BaseHandler<GetCircleDetailQuery, CircleResponse, Result<CircleResponse>>
{
	public GetCircleDetailHandler(AuthUtils authUtils, Touhou_Songs_Context context) : base(authUtils, context) { }

	public override async Task<Result<CircleResponse>> Handle(GetCircleDetailQuery query, CancellationToken cancellationToken)
	{
		var circle_Res = await _context.Circles
			.Include(c => c.ArrangementSongs)
			.Where(c => c.Name == query.Name)
			.Select(c => new CircleResponse(c.Id, c.Name, c.Status.ToString())
			{
				ArrangementSongTitles = c.ArrangementSongs.Select(a => a.Title),
			})
			.SingleOrDefaultAsync();

		if (circle_Res is null)
		{
			throw new AppException(HttpStatusCode.NotFound, $"Circle {query.Name} not found.");
		}

		return Ok(circle_Res);
	}
}
