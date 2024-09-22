namespace Touhou_Songs.Infrastructure.Url;

public static class UrlUtils
{
	public static string? GetYoutubeThumbnailUrl(string url)
	{
		var isYoutubeUrl = url.Contains("youtu");

		if (!isYoutubeUrl)
		{
			return null;
		}

		var isLongUrl = url.Contains("/watch?v=");
		var videoId = isLongUrl
			? url.Split("watch?v=")[1].Split("&")[0]
			: url.Split("youtu.be/")[1].Split("&")[0];

		return $"https://img.youtube.com/vi/{videoId}/0.jpg";
	}
}
