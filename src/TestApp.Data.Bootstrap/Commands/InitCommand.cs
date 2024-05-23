using CommandLine;
using TestApp.Data.Bootstrap.Service;

namespace TestApp.Data.Bootstrap.Commands;

[Verb("init", isDefault: false, HelpText = "Команда генерации тестовых данных.")]
internal sealed class InitCommand : ICommand
{
    [Option('h', "host", Required = true, HelpText = "Адресс сервиса API.")]
    public string Host { get; set; }


    [Option('c', "count", Required = true, Default = 100, HelpText = "Количество записией для генерации.")]
    public byte Count { get; set; }

    public void Execute()
    {
        var resourceService = new ResourceService();
        var dataGenerationService = new DataGenerationService(resourceService);
        using var patientService = new PatientService(Host);
        var data = dataGenerationService.Generate(Count);
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 3
        };

        var task = Parallel.ForEachAsync(data, options, async (item, token) =>
        {
            await patientService.Create(item, token);

            Console.WriteLine($"Name: {item.Name.Given[0]}, Family: {item.Name.Family}, Patronymic: {item.Name.Given[1]}");
        });

        task.Wait();
    }
}