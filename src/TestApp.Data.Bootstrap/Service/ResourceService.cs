using System.Text.Json;
using TestApp.Data.Bootstrap.Models;

namespace TestApp.Data.Bootstrap.Service;

internal sealed class ResourceService
{
    public ResourceService()
    {
        Name = LoadResource("name");
        Family = LoadResource("family");
        Patronymic = LoadResource("patronymic");
    }

    private static ResourceModel LoadResource(string name)
    {
        var assembly = typeof(ResourceService).Assembly;
        using var stream = assembly.GetManifestResourceStream($"TestApp.Data.Bootstrap.Resources.{name}.json");
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        return JsonSerializer.Deserialize<ResourceModel>(json);
    }

    public ResourceModel Name { get; }
    
    public ResourceModel Family { get; }

    public ResourceModel Patronymic { get; }
}