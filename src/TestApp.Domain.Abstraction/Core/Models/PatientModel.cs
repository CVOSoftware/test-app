namespace TestApp.Domain.Abstraction.Core.Models;

public sealed record PatientModel(PatientNameModel Name, Gender Gender, DateTime BirthDate, bool Active);