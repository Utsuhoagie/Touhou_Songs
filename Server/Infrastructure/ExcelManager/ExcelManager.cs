using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Touhou_Songs.Infrastructure.BaseEntities;

namespace Touhou_Songs.Infrastructure.ExcelManager;

public class ExcelManager
{
	public const string MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

	public byte[] GenerateFromEntities(List<string> headers, IEnumerable<BaseEntity> entities, string? sheet1Name = null)
	{
		var excelFile = new XSSFWorkbook();

		var sheet1 = excelFile.CreateSheet(sheet1Name);

		var headerRow = sheet1.CreateRow(0);
		for (var i = 0; i < headers.Count; i++)
		{
			var header = headers[i];
			var cell = headerRow.CreateCell(i);

			cell.SetCellType(CellType.String);
			cell.SetCellValue(header);
		}

		for (var i = 1; i < entities.Count(); i++)
		{
			var entity = entities.ElementAt(i - 1);

			var row = sheet1.CreateRow(i);

			for (var j = 0; j < headers.Count; j++)
			{
				var cell = row.CreateCell(j);
				var header = headers[j];

				var entityProperty = entity
					.GetType()
					.GetProperty(header);

				var cellValue = entityProperty
					?.GetValue(entity);

				cell.SetCellType(CellType.String);
				cell.SetCellValue(cellValue?.ToString());
			}
		}

		for (var i = 0; i < headers.Count; i++)
		{
			sheet1.AutoSizeColumn(i);
		}

		using var memoryStream = new MemoryStream();
		excelFile.Write(memoryStream);

		return memoryStream.ToArray();
	}
}
