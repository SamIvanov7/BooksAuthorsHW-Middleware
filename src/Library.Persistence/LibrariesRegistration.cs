using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistence;

public static class LibrariesRegistration
{
    private const string ConnectionStringName = "Libraries";

    public static void AddLibrariesPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
                               ?? throw new AggregateException($"Connection string: '{ConnectionStringName}' is not found in configurations.");

        services.AddDbContext<LibrariesDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        LibrariesDbContext.LibDbMigrationsHistoryTable,
                        LibrariesDbContext.LibDbSchema);
                });
        });
    }
}