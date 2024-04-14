using Library.Core.Domain.Bocks.Common;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Core.Domain.Bocks.Common;

public class AuthorMustExistChecker(LibrariesDbContext librariesDbContext) : IAuthorMustExistChecker
{
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await librariesDbContext.Authors.AnyAsync(a => a.Id == id, cancellationToken);
    }
}