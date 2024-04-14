using Library.Core.Domain.Bocks.Models;

namespace Library.Core.Domain.Bocks.Common;

public interface IBocksAuthorsRepository
{
    public void Add(BockAuthor bockAuthor);

    public Task<BockAuthor> FindBockAuthorAsync(Guid bockId, Guid authorId, CancellationToken cancellationToken);

    public void Delete(BockAuthor bockAuthor);
}