using FluentValidation;
using PatientApi.Logic.Models;

namespace PatientApi.Logic.Validators
{
    public class SearchByBirthDateRequestValidator : AbstractValidator<SearchByBirthDateRequest>
    {
        private readonly string[] prefixes = {"eq","ne","gt","lt","ge","le","sa","eb","ap"};

        public SearchByBirthDateRequestValidator()
        {
            RuleFor(r => r)
                .NotNull()
                .Must(f => DateFilterValid(f.StartFilter) || DateFilterValid(f.EndFilter)).WithMessage("Date filter is not valid. One of the filters must have the correct format");
        }

        private bool ContainsPrefix(string input) => prefixes.Contains(input?.Substring(0,2)?.ToLower());

        private bool DateFilterValid(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; 
            }

            var dateStr = ContainsPrefix(input) ? input.Substring(2) : input;

            return DateTime.TryParse(dateStr, out var date) || (dateStr.Length == 4 && DateTime.TryParse($"{dateStr}-01", out var yearDate));
        }
    }
}
