using System.Text.Json.Serialization;

namespace TestApp.Data.Bootstrap.Models;

internal sealed record ResourceModel
{
    [JsonPropertyName("male")]
    public string[] Male { get; init; }

    [JsonPropertyName("female")]
    public string[] Female { get; init; }
}