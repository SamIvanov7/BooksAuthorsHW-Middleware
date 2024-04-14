using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Authors.Models;

namespace Library.Core.Domain.Bocks.Rules;

internal class AuthorMustExistRule(
    Guid authorId, 
    IAuthorMustExistChecker authorMustExistChecker)
    : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await authorMustExistChecker.ExistsAsync(authorId, cancellationToken);
        return Check(exists);
    }

    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failed($"{nameof(Author)} was not found. {nameof(Author.Id)}: '{authorId}'.");
    }
}