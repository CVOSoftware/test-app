namespace TestApp.Domain.Abstraction.Exceptions;

public sealed class DomainException : Exception
{
    private const string NotFoundId = "5058855a-bcc7-41d1-8a8c-5ebc255a1eb4";

    internal DomainException(string id, string? message = null) : base(message)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    public string Id { get; }

    public static bool NotFound(DomainException exception) => exception.Id == NotFoundId;

    public static DomainException NotFound() => new (NotFoundId);
}