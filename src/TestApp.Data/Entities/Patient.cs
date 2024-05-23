namespace TestApp.Data.Entities;

public sealed class Patient
{
    public Guid Id { get; init; }

    public string? Use { get; set; }

    public string Name { get; set; }

    public string Family { get; set; }

    public string Patronymic { get; set; }

    public byte Gender { get; set; }

    public bool IsActive { get; set; }

    public DateTime DoB { get; set; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; set; }
}