using FluentValidation;
using Library.Core.Domain.Bocks.Data;
using Library.Core.Domain.Bocks.Common;
using FluentValidation.Results;
using Library.Core.Domain.Bocks.Models;
using Library.Core.Domain.Bocks.Rules;

namespace Library.Core.Domain.Bocks.Validators;

internal class CreateBockAuthorValidator : AbstractValidator<CreateBockAuthorData>
{
    public CreateBockAuthorValidator(
        IAuthorMustExistChecker authorMustExistChecker,
        IBockMustExistChecker bockMustExistChecker)
    {
        RuleFor(x => x.BockId)
            .NotEmpty()
            .WithMessage("BockId is required.");

        RuleFor(x => x.BockId)
            .CustomAsync(async (bockId, context, cancellationToken) =>
            {
                var ruleResult =
                    await new BockMustExistRule(bockId, bockMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new ValidationFailure(nameof(Bock), error));
            });

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required.");

        RuleFor(x => x.AuthorId)
            .CustomAsync(async (authorId, context, cancellationToken) =>
            {
                var ruleResult =
                    await new AuthorMustExistRule(authorId, authorMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new ValidationFailure(nameof(Bock), error));
            });
    }
}