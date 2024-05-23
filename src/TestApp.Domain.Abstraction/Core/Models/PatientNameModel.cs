namespace TestApp.Domain.Abstraction.Core.Models;

public record PatientNameModel(Guid? Id, string? Use, string Family, string[] Given);