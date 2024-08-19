using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExcelBuilder;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record ExportAllDataQuery : IRequest<Result<FileResponse>>
{
}

class ExportAllDataHandler : BaseHandler<ExportAllDataQuery, FileResponse>
{
	private readonly ExcelBuilder _excelBuilder = new();

	record OfficialSongColumnNames
	{
		public const string Id = "Id";
		public const string Title = "Title";
		public const string Context = "Context";

		public const string GameId = "GameId";

		public readonly static List<string> All = [Id, Title, Context, GameId];
	}

	record OfficialGameColumnNames
	{
		public const string Id = "Id";
		public const string Title = "Title";
		public const string GameCode = "GameCode";
		public const string NumberCode = "NumberCode";
		public const string ReleaseDate = "ReleaseDate";
		public const string ImageUrl = "ImageUrl";


		public readonly static List<string> All = [Id, Title, GameCode, NumberCode, ReleaseDate, ImageUrl];
	}

	record CharacterColumnNames
	{
		public const string Id = "Id";
		public const string Name = "Name";
		public const string ImageUrl = "ImageUrl";

		public const string OriginGameId = "OriginGameId";

		public readonly static List<string> All = [Id, Name, ImageUrl, OriginGameId];
	}

	record CircleColumnNames
	{
		public const string Id = "Id";
		public const string Name = "Name";
		public const string Status = "Status";

		public readonly static List<string> All = [Id, Name, Status];
	}

	record ArrangementSongColumnNames
	{
		public const string Id = "Id";
		public const string Title = "Title";
		public const string TitleRomaji = "TitleRomaji";
		public const string TitleJapanese = "TitleJapanese";
		public const string Url = "Url";
		public const string Status = "Status";

		public const string CircleId = "CircleId";


		public readonly static List<string> All = [Id, Title, TitleRomaji, TitleJapanese, Url, Status, CircleId];
	}

	public ExportAllDataHandler(AuthUtils authUtils, AppDbContext context) : base(authUtils, context) { }

	public async override Task<Result<FileResponse>> Handle(ExportAllDataQuery query, CancellationToken cancellationToken)
	{
		await ExportOfficialSongs();
		await ExportOfficialGames();
		await ExportCharacters();
		await ExportCircles();
		await ExportArrangementSongs();

		var excelFile = _excelBuilder.GetFile();

		return _resultFactory.Ok(new()
		{
			Contents = excelFile,
			FileName = "ExportAllData.xlsx",
			MimeType = ExcelBuilder.MimeType,
		});
	}

	private async Task ExportOfficialSongs()
	{
		var dbOfficialSongs = await _context.OfficialSongs
			.ToListAsync();

		var headers = OfficialSongColumnNames.All;

		var sheetName = "OfficialSongs";

		_excelBuilder.GenerateFromEntities(sheetName, headers, dbOfficialSongs);
	}

	private async Task ExportOfficialGames()
	{
		var dbOfficialGames = await _context.OfficialGames
			.ToListAsync();

		var headers = OfficialGameColumnNames.All;

		var sheetName = "OfficialGames";

		_excelBuilder.GenerateFromEntities(sheetName, headers, dbOfficialGames);
	}

	private async Task ExportCharacters()
	{
		var dbCharacters = await _context.Characters
			.ToListAsync();

		var headers = CharacterColumnNames.All;

		var sheetName = "Characters";

		_excelBuilder.GenerateFromEntities(sheetName, headers, dbCharacters);
	}

	private async Task ExportCircles()
	{
		var dbCircles = await _context.Circles
			.ToListAsync();

		var headers = CircleColumnNames.All;

		var sheetName = "Circles";

		_excelBuilder.GenerateFromEntities(sheetName, headers, dbCircles);
	}

	private async Task ExportArrangementSongs()
	{
		var dbArrangementSongs = await _context.ArrangementSongs
			.ToListAsync();

		var headers = ArrangementSongColumnNames.All;

		var sheetName = "ArrangementSongs";

		_excelBuilder.GenerateFromEntities(sheetName, headers, dbArrangementSongs);
	}
}
