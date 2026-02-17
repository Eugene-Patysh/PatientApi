namespace PatientApi.Logic.Validators
{
    public interface ICustomValidator<T>
    {
        public Task ValidateAsync(T objectDto, string ruleSetName);
    }
}
