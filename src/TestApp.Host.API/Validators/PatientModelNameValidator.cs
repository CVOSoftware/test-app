using FluentValidation;
using TestApp.Domain.Abstraction.Core.Models;
using TestApp.Host.API.Extensions;

namespace TestApp.Host.API.Validators;

public sealed class PatientModelNameValidator : AbstractValidator<PatientNameModel>
{
    public PatientModelNameValidator()
    {
        RuleFor(p => p.Use)
            .MaximumLength(300)
            .SetName("use");

        RuleFor(p => p.Family)
            .NotEmpty()
            .MaximumLength(30)
            .NotWhitespace()
            .SetName("family");

        RuleFor(p => p.Given)
            .NotNull()
            .ForEach(p => p.NotEmpty().MaximumLength(30))
            .SetName("given");

        RuleFor(p => p.Given.Length)
            .GreaterThanOrEqualTo(2)
            .LessThanOrEqualTo(2)
            .SetName("given");

    }
}