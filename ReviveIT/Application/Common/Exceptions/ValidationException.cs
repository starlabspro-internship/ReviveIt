using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, IEnumerable<string>> Failures { get; }

        public ValidationException() : base("One or more validation failures occurred.")
        {
            Failures = new Dictionary<string, IEnumerable<string>>();
        }

        public ValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(x => x.PropertyName == propertyName)
                    .Select(x => x.ErrorMessage).ToList();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
