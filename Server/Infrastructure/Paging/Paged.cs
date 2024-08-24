namespace Touhou_Songs.Infrastructure.Paging;

public record Paged<T>
{
	public required int TotalItemsCount { get; set; }
	public required List<T> Items { get; set; } = [];

	public int CurrentPage { get; set; }
	public int PageSize { get; set; }

	public int TotalPages => (TotalItemsCount - 1 + PageSize) / PageSize;
	public bool HasNext => CurrentPage < TotalPages;
	public bool HasPrev => CurrentPage > 1;

	public Paged(PagingParams query) => (CurrentPage, PageSize) = (query.Page, query.PageSize);
}
