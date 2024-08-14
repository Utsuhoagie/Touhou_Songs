using MediatR;
using Microsoft.EntityFrameworkCore;
using Touhou_Songs.Data;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseHandler;
using Touhou_Songs.Infrastructure.ExcelManager;
using Touhou_Songs.Infrastructure.Results;

namespace Touhou_Songs.App.DataManagement.Features;

public record ExportAllOfficialSongsQuery : IRequest<Result<FileResponse>>
{
}

class ExportAllOfficialSongsHandler : BaseHandler<ExportAllOfficialSongsQuery, FileResponse>
{
	private readonly ExcelManager _excelManager;

	record ColumnNames
	{
		public const string Id = "Id";
		public const string Title = "Title";
		public const string Context = "Context";

		public const string GameId = "GameId";

		public readonly static List<string> All = [Id, Title, Context, GameId];
	}

	public ExportAllOfficialSongsHandler(AuthUtils authUtils, AppDbContext context, ExcelManager excelManager) : base(authUtils, context)
		=> _excelManager = excelManager;

	public async override Task<Result<FileResponse>> Handle(ExportAllOfficialSongsQuery query, CancellationToken cancellationToken)
	{
		var dbOfficialSongs = await _context.OfficialSongs
			.ToListAsync();

		var headers = ColumnNames.All;

		var sheet1Name = "OfficialSongs";

		var excelFile = _excelManager.GenerateFromEntities(headers, dbOfficialSongs, sheet1Name);

		return _resultFactory.Ok(new()
		{
			Contents = excelFile,
			FileName = "ExportAllOfficialSongs.xlsx",
			MimeType = ExcelManager.MimeType,
		});
	}
}
