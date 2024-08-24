namespace Touhou_Songs.Infrastructure.Paging;

public record PagingParams
{
	public const int DEFAULT_PAGE_SIZE = 10;

	public int Page { get; set; } = 1;
	public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;
}
