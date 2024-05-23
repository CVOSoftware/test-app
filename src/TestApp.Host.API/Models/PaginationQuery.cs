using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestApp.Host.API.Models;

public class PaginationQuery
{
    public const string NAME_PAGE = "page";

    public const string NAME_LIMIT = "limit";

    public const int MIN_PAGE = 1;

    public const int LIMIT_DEFAULT = 10;

    public PaginationQuery()
    {
        MinLimit = 1;
        MaxLimit = 100;
    }

    public PaginationQuery(int minLimit, int maxLimit)
    {
        MinLimit = minLimit;
        MaxLimit = maxLimit;
    }

    [FromQuery(Name = NAME_PAGE)]
    [DefaultValue(MIN_PAGE)]
    public int Page { get; init; } = MIN_PAGE;

    [FromQuery(Name = NAME_LIMIT)]
    [DefaultValue(LIMIT_DEFAULT)]
    public virtual int Limit { get; init; } = LIMIT_DEFAULT;

    [BindNever]
    public int MinLimit { get; }

    [BindNever]
    public int MaxLimit { get; }

    [BindNever]
    public int Offset => (Page - 1) * Limit;
}