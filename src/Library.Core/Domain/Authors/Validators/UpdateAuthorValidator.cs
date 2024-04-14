using FluentValidation;
using Library.Core.Domain.Authors.Data;

namespace Library.Core.Domain.Authors.Validators;

internal class UpdateAuthorValidator : AbstractValidator<UpdateAuthorData>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x=>x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x=>x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x=>x.MiddleName)
            .MaximumLength(100);
    }
}