using Library.Core.Common;
using Library.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Processing;

//todo: cover it with unit tests
internal class EnumerationIgnorer(LibrariesDbContext librariesDbContext) : IEnumerationIgnorer
{
    public void IgnoreEnumerations()
    {
        var enumerationEntries = librariesDbContext
            .ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEnumeration && e.State != EntityState.Unchanged);

        foreach (var enumerationEntry in enumerationEntries)
        {
            enumerationEntry.State = EntityState.Unchanged;
        }
    }
}
