using FluentValidation;
using TestApp.Host.API.Extensions;
using TestApp.Host.API.Models;

namespace TestApp.Host.API.Validators;

public abstract class PaginationQueryValidator<TQuery> : AbstractValidator<TQuery>
    where TQuery : PaginationQuery
{
    protected PaginationQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThanOrEqualTo(PaginationQuery.MIN_PAGE)
            .LessThanOrEqualTo(int.MaxValue)
            .SetName(PaginationQuery.NAME_PAGE);

        RuleFor(p => p.Limit)
            .GreaterThanOrEqualTo(q => q.MinLimit)
            .LessThanOrEqualTo(q => q.MaxLimit)
            .SetName(PaginationQuery.NAME_LIMIT);
    }
}

public sealed class PaginationQueryValidator : PaginationQueryValidator<PaginationQuery>
{
    public PaginationQueryValidator() : base()
    {

    }
}