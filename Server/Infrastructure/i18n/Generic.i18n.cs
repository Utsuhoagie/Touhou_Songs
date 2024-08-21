namespace Touhou_Songs.Infrastructure.i18n;

public static class GenericI18n
{
	public static readonly I18nText None = new("None", "Không có");

	public static readonly I18nTextWithParams Success = new("Success: \"{0}\"", "Thao tác thành công: \"{0}\"");

	public static readonly I18nTextWithParams Error = new("Error during \"{0}\": \"{1}\"", "Có lỗi xảy ra khi \"{0}\": \"{1}\"");

	public static readonly I18nTextWithParams NotFound = new("{0} not found: \"{1}\"", "Không tìm thấy {0}: \"{1}\"");

	public static readonly I18nTextWithParams BadRequest = new("Request is invalid: \"{0}\"", "Yêu cầu không hợp lệ: \"{0}\"");

	public static readonly I18nTextWithParams Conflict = new("Conflict with: \"{0}\"", "Xung đột với: \"{0}\"");
}
