using System.Text.RegularExpressions;
using FluentValidation;

namespace TestApp.Host.API.Extensions;

internal static class IRuleBuilderExtension
{
    public static IRuleBuilderOptions<T, string> NotWhitespace<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.Matches("^(?!\\s).*(?!\\s).$", RegexOptions.Compiled);
}