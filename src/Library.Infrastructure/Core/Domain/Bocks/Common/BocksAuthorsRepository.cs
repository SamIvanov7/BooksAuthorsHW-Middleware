using Library.Core.Domain.Bocks.Common;
using Library.Core.Domain.Bocks.Models;
using Library.Core.Exceptions;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Core.Domain.Bocks.Common;

public class BocksAuthorsRepository(LibrariesDbContext librariesDbContext) : IBocksAuthorsRepository
{
    public void Add(BockAuthor bockAuthor)
    {
        librariesDbContext.BocksAuthors.Add(bockAuthor);
    }

    public async Task<BockAuthor> FindBockAuthorAsync(Guid bockId, Guid authorId, CancellationToken cancellationToken = default)
    {
        return await librariesDbContext.BocksAuthors.SingleOrDefaultAsync(x => x.BockId == bockId && x.AuthorId == authorId, cancellationToken)
            ?? throw new NotFoundException($"`{nameof(BockAuthor)}` was not found.");
    }

    public void Delete(BockAuthor bockAuthor)
    {
        librariesDbContext.BocksAuthors.Remove(bockAuthor);
    }
}