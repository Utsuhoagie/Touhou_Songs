using Touhou_Songs.Infrastructure.Auth;

namespace Touhou_Songs.App.UserProfile;

public class UserProfile
{
	public int Id { get; set; }

	public string Bio { get; set; } = "Write something about yourself!";

	public string? AvatarUrl { get; set; }

	public required string UserId { get; set; } = string.Empty;
	public required AppUser User { get; set; } = default!;

	public UserProfile() { }

	public void Update(string? bio = null, string? avatarUrl = null)
	{
		Bio = bio ?? Bio;
		AvatarUrl = avatarUrl ?? AvatarUrl;
	}
}
