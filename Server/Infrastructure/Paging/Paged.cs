namespace Touhou_Songs.Infrastructure.Paging;

public record Paged<T>
{
	public required int TotalItemsCount { get; set; }
	public required List<T> Items { get; set; } = [];

	public required int PageSize { get; set; }
	public required int CurrentPage { get; set; }

	public int TotalPages => (TotalItemsCount - 1 + PageSize) / PageSize;
	public bool HasNext => CurrentPage < TotalPages;
	public bool HasPrev => CurrentPage > 1;
}
