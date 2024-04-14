namespace Library.Core.Domain.Bocks.Common;

public interface IBockMustExistChecker
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}