using Touhou_Songs.App.TierListMaking;
using Touhou_Songs.Infrastructure.Auth;
using Touhou_Songs.Infrastructure.BaseEntities;

namespace Touhou_Songs.App.UserProfile;

public class UserProfile : BaseAuditedEntity
{
	public string Bio { get; set; } = "Write something about yourself!";

	public string? AvatarUrl { get; set; }

	public required string UserId { get; set; } = string.Empty;
	public required AppUser User { get; set; } = default!;

	public List<TierList> TierLists { get; set; } = new();

	public UserProfile() { }

	public void Update(string? bio = null, string? avatarUrl = null)
	{
		Bio = bio ?? Bio;
		AvatarUrl = avatarUrl ?? AvatarUrl;
	}

	public void AddTierList(TierList tierList)
	{
		TierLists.Add(tierList);
	}
}
