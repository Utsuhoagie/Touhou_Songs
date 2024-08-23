namespace Touhou_Songs.Infrastructure.i18n;

public static class GenericI18n
{
	public static readonly I18nText None = new("None", "Không có");

	/// <summary>
	/// Success: "{0}"
	/// </summary>
	public static readonly I18nTextWithParams Success = new("Success: \"{0}\"", "Thao tác thành công: \"{0}\"");

	/// <summary>
	/// Error during "{0}": "{1}"
	/// </summary>
	public static readonly I18nTextWithParams ErrorDuring = new("Error during \"{0}\": \"{1}\"", "Có lỗi xảy ra khi \"{0}\": \"{1}\"");

	/// <summary>
	/// {0} not found: "{1}"
	/// </summary>
	public static readonly I18nTextWithParams NotFound = new("{0} not found: \"{1}\"", "Không tìm thấy {0}: \"{1}\"");

	/// <summary>
	/// Request is invalid: "{0}"
	/// </summary>
	public static readonly I18nTextWithParams BadRequest = new("Request is invalid: \"{0}\"", "Yêu cầu không hợp lệ: \"{0}\"");

	/// <summary>
	/// Conflict with: "{0}"
	/// </summary>
	public static readonly I18nTextWithParams Conflict = new("Conflict: \"{0}\"", "Xung đột: \"{0}\"");
}
