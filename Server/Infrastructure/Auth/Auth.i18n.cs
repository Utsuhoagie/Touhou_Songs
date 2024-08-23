using Touhou_Songs.Infrastructure.i18n;

namespace Touhou_Songs.Infrastructure.Auth;

public static class AuthI18n
{
	#region Success
	public static readonly I18nText ChangedPasswordSuccess = new("Changed password successfully", "Đổi mật khẩu thành công");
	#endregion

	#region Errors
	/// <summary>
	/// Bad role: "{0}"
	/// </summary>
	public static readonly I18nTextWithParams BadRole = new("Bad role: \"{0}\"", "Quyền không hợp lệ: \"{0}\"");

	public static readonly I18nText OnlyOneAdmin = new("Can't register more than 1 Admin", "Không được tạo nhiều hơn 1 Admin");

	/// <summary>
	/// Email "{0}" already exists
	/// </summary>
	public static readonly I18nTextWithParams EmailAlreadyExists = new("Email \"{0}\" already exists", "Email \"{0}\" đã tồn tại");

	public static readonly I18nText ConfigsNotFound = new("Configs not found. Contact the developer.", "Không tìm thấy bộ tùy chỉnh cho đăng nhập. Liên hệ với nhà phát triển.");

	public static readonly I18nText WrongLoginInfo = new("Wrong login info", "Sai thông tin đăng nhập");

	/// <summary>
	/// No "{0}" claim
	/// </summary>
	public static readonly I18nTextWithParams NoClaim = new("No \"{0}\" claim", "Không tìm thấy \"{0}\" của người đang đăng nhập");
	#endregion
}
