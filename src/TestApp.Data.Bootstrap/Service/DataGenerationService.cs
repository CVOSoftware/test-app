using System.Security.Cryptography;
using TestApp.Data.Bootstrap.Models;
using TestApp.Domain.Abstraction.Core.Models;

namespace TestApp.Data.Bootstrap.Service;

internal sealed class DataGenerationService
{
    private readonly ResourceService _resourceService;

    public DataGenerationService(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    public PatientModel[] Generate(byte count)
    {
        var models = new PatientModel[count];
        var random = new Random();

        for (var i = 0; i < count; i++)
        {
            var gender = random.Next(0, 2) == 0 ? Gender.Male : Gender.Female;
            var active = random.Next(0, 2) == 1;

            var name = GetResource(_resourceService.Name, gender, random);
            var family = GetResource(_resourceService.Family, gender, random);
            var patronymic = GetResource(_resourceService.Patronymic, gender, random);
            var birthDate = GetDate(random);

            var nameModel = new PatientNameModel(null, null, family, new[] { name, patronymic });
            var model = new PatientModel(nameModel, gender, birthDate, active);

            models[i] = model;
        }

        return models;
    }

    private static string GetResource(ResourceModel model, Gender gender, Random random)
    {
        var resources = gender switch
        {
            Gender.Male => model.Male,
            Gender.Female => model.Female,
            _ => throw new NotImplementedException()
        };

        var index = random.Next(0, resources.Length);

        return resources[index];
    }

    private static DateTime GetDate(Random random)
    {
        var now = DateTime.UtcNow;
        var start = new DateTime(2020, 1, 1, 0, 0, 0);
        var diff = now - start;

        return start.AddDays(random.Next(diff.Days))
            .AddHours(random.Next(diff.Hours))
            .AddMinutes(random.Next(diff.Minutes))
            .ToUniversalTime();
    }
}