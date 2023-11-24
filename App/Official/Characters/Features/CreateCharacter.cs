﻿using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExceptionHandling;

namespace Touhou_Songs.App.Official.Characters.Features
{
	public record CreateCharacterCommand : IRequest<string>
	{
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		public string OriginGameCode { get; set; }
		public required List<string> SongTitles { get; set; }

		public CreateCharacterCommand(string name, string imageUrl, string originGameCode)
			=> (Name, ImageUrl, OriginGameCode) = (name, imageUrl, originGameCode);
	}

	class CreateCharacterCommandHandler : BaseHandler<CreateCharacterCommand, string>
	{
		public CreateCharacterCommandHandler(IHttpContextAccessor httpContextAccessor, Touhou_Songs_Context context) : base(httpContextAccessor, context) { }

		public override async Task<string> Handle(CreateCharacterCommand command, CancellationToken cancellationToken)
		{
			var originGame = await _context.OfficialGames
				.SingleOrDefaultAsync(og => EF.Functions.ILike(og.GameCode, $"{command.OriginGameCode}"));

			if (originGame is null)
			{
				throw new AppException(HttpStatusCode.NotFound, $"Official OriginGame {command.OriginGameCode} not found");
			}

			var officialSongs = await _context.OfficialSongs
				.Where(os => command.SongTitles.Contains(os.Title))
				.ToListAsync();

			var character = new Character(command.Name, command.ImageUrl)
			{
				OriginGameId = originGame.Id,
				OriginGame = originGame,
				OfficialSongs = officialSongs,
			};

			_context.Characters.Add(character);
			await _context.SaveChangesAsync();

			return character.Name;
		}
	}
}