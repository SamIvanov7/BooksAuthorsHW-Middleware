using Library.Core.Common;
using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Models;

namespace Library.Core.Domain.Bocks.Rules;

internal class BockMustExistRule(
    Guid bockId,
    IBockMustExistChecker bockMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await bockMustExistChecker.ExistsAsync(bockId, cancellationToken);
        return Check(exists);
    }

    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failed($"{nameof(Bock)} was not found. {nameof(Bock.Id)}: '{bockId}'.");
    }
}