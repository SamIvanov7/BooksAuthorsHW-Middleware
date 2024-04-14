using Library.Core.Common;
using Library.Infrastructure.Processing;
using Library.Persistence;

namespace Library.Infrastructure.Core.Common;

internal class UnitOfWork(
    LibrariesDbContext librariesDbContext,
    IEnumerationIgnorer enumerationIgnorer)
    : IUnitOfWork
{
    private readonly LibrariesDbContext _librariesDbContext = librariesDbContext ?? throw new ArgumentNullException(nameof(librariesDbContext));

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        enumerationIgnorer.IgnoreEnumerations();
        return await _librariesDbContext.SaveChangesAsync(cancellationToken);
    }
}
