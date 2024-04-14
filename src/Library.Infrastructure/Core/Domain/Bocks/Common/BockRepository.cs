using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Models;
using Library.Core.Exceptions;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Core.Domain.Bocks.Common;

public class BockRepository(LibrariesDbContext librariesDbContext) : IBocksRepository
{
    public async Task<Bock> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        return await librariesDbContext
               .Bocks
               .Include(x => x.BocksAuthors)
               .ThenInclude(x => x.Author)
               .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
               ?? throw new NotFoundException($"{nameof(Bock)} with id: '{id}' was not found.");
    }

    public async Task<IReadOnlyCollection<Bock>> FindManyAsync(
        IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await librariesDbContext.Bocks.Where(b => ids.Contains(b.Id)).ToArrayAsync(cancellationToken);
    }

    public void Add(Bock bock)
    {
        librariesDbContext.Bocks.Add(bock);
    }

    public void Delete(IReadOnlyCollection<Bock> bocks)
    {
        librariesDbContext.Bocks.RemoveRange(bocks);
    }
}