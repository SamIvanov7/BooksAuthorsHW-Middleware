using FluentValidation;
using Library.Core.Domain.Bocks.Data;

namespace Library.Core.Domain.Bocks.Validators;

internal class CreateBockValidator : AbstractValidator<CreateBockData>
{
    public CreateBockValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(2000)
            .WithMessage("Description must not exceed 2000 characters.");
    }
}