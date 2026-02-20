using FluentValidation;
using PatientApi.Logic.Models.Exceptions;

namespace PatientApi.Logic.Validators
{
    public class CustomValidator<T> : ICustomValidator<T>
    {
        private readonly IValidator<T> _validator;

        public CustomValidator(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task ValidateAsync(T objectDto, string ruleSetName)
        {
            var validationResult = await _validator.ValidateAsync(objectDto, v => v.IncludeRulesNotInRuleSet().IncludeRuleSets(ruleSetName));

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                throw new AppValidationException("Bad request", errors);
            }
        }
    }
}
