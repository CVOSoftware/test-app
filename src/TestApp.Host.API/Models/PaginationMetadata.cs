namespace TestApp.Host.API.Models;

public sealed class PaginationMetadata
{
    public PaginationMetadata(PaginationQuery paginationQuery, int totalCount, int selectedCount)
    {
        Page = paginationQuery.Page;
        Limit = paginationQuery.Limit;
        PageCount = (int)Math.Ceiling((double)totalCount / paginationQuery.Limit);
        SelectedCount = selectedCount;
        TotalCount = totalCount;
    }

    public int Page { get; }

    public int Limit { get; }

    public int PageCount { get; }

    public int TotalCount { get; }

    public int SelectedCount { get; }
}