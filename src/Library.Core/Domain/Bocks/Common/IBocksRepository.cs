using Library.Core.Domain.Bocks.Models;

namespace Library.Core.Domain.Bocks.Common;

public interface IBocksRepository
{
    public Task<Bock> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Bock>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Bock bock);

    public void Delete(IReadOnlyCollection<Bock> bocks);
}