using FluentValidation;
using PatientApi.Logic.Models;

namespace PatientApi.Logic.Validators
{
    public class PatientValidator : AbstractValidator<PatientDto>
    {
        public PatientValidator()
        {
            RuleFor(p => p).NotNull().WithMessage("patient model can't be null.");
            RuleFor(p => p.Name).NotNull().WithMessage("Name info can't be null.");
            RuleFor(p => p.Name.Family).Must(f => !string.IsNullOrEmpty(f)).WithMessage("Family name can't be null.");
            RuleFor(p => p.BirthDate).Must(date => date != default(DateTime)).WithMessage("Birth date can't be null.");

            RuleSet("Add", () =>
            {
                RuleFor(p => p.Id).Null().WithMessage("Patient Id should be null.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(p => p.Id).NotNull().WithMessage("Patient Id can't be null.");
                RuleFor(p => p.Name.Id).Must(id => !string.IsNullOrEmpty(id)).WithMessage("Name id can't be null.");
            });
        }
    }
}
