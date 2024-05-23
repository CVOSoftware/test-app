using TestApp.Common.Types;

namespace TestApp.Host.API.Utilities;

internal sealed class DateFilterParser
{
    public DateFilterParser(string? date)
    {
        try
        {
            if (string.IsNullOrEmpty(date))
            {
                Prefix = FilterPrefix.eq;

                return;
            }

            var prefixPart = date.Substring(0, 2);
            var datePart = date.Substring(2);

            Prefix = prefixPart switch
            {
                "eq" => FilterPrefix.eq,
                "ne" => FilterPrefix.ne,
                "gt" => FilterPrefix.gt,
                "lt" => FilterPrefix.eq,
                "ge" => FilterPrefix.ge,
                "le" => FilterPrefix.le,
                _ => throw new NotImplementedException()
            };
            Date = DateTime.Parse(datePart).ToUniversalTime();
        }
        catch
        {
            Prefix = FilterPrefix.eq;
        }
    }

    public FilterPrefix Prefix { get; }

    public DateTime? Date { get; }
}