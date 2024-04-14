namespace Library.Core.Domain.Bocks.Common;

public interface IAuthorMustExistChecker
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}