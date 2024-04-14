using Library.Core.Domain.Authors.Models;

namespace Library.Core.Domain.Authors.Common;

public interface IAuthorsRepository
{
    public Task<Author> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Author>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Author author);

    public void Delete(IReadOnlyCollection<Author> authors);
}