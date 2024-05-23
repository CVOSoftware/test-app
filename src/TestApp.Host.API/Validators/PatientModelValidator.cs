using FluentValidation;
using TestApp.Domain.Abstraction.Core.Models;
using TestApp.Host.API.Extensions;

namespace TestApp.Host.API.Validators;

public sealed class PatientModelValidator : AbstractValidator<PatientModel>
{
    public PatientModelValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .SetValidator(new PatientModelNameValidator())
            .SetName("name");

        RuleFor(p => p.Gender)
            .NotNull()
            .SetName("gender");

        RuleFor(p => p.BirthDate)
            .NotNull()
            .SetName("birthDate");

        RuleFor(p => p.Active)
            .NotNull()
            .SetName("active");
    }
}