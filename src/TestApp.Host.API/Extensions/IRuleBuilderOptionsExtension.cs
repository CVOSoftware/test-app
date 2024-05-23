using FluentValidation;

namespace TestApp.Host.API.Extensions;

internal static class IRuleBuilderOptionsExtension
{
    public static IRuleBuilderOptions<T, TProperty> SetName<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string name)
        => rule.OverridePropertyName(name).WithName(name);
}