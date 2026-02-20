using System.Globalization;

namespace PatientApi.Logic.Models.Exceptions
{
    public class AppValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public AppValidationException() : base() { }

        public AppValidationException(string message) : base(message) { }

        public AppValidationException(string message, IDictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
        }

        public AppValidationException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) {}
    }
}
