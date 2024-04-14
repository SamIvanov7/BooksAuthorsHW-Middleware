using FluentValidation.Results;

namespace Library.Core.Exceptions;

public class OldValidationException(string message) : DomainException(message)
{
    // todo: think how to change ValidationFailure and do we need to do it
    public OldValidationException(List<ValidationFailure> failures) : this("Validation is failed.")
    {
        Failures = failures.AsReadOnly();
    }

    public IReadOnlyCollection<ValidationFailure> Failures { get; }
}
