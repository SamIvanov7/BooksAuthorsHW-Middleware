using Library.Core.Domain.Bocks.Common;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Core.Domain.Bocks.Common;

public class BockMustExistChecker(LibrariesDbContext librariesDbContext) : IBockMustExistChecker
{
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await librariesDbContext.Bocks.AnyAsync(b => b.Id == id, cancellationToken);
    }
}