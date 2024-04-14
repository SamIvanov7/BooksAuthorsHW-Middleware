using Library.Core.Domain.Authors.Models;
using Library.Core.Domain.Bocks.Models;
using Library.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence;

public class LibrariesDbContext(DbContextOptions<LibrariesDbContext> options) : DbContext(options)
{
    internal const string LibDbSchema = "libdb";
    internal const string LibDbMigrationsHistoryTable = "__LiDdbMigrationsHistory";

    public DbSet<Bock> Bocks { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<BockAuthor> BocksAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // todo: do it only for local development
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(LibDbSchema);
        modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BockEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BocksAuthorsEntityTypeConfiguration());
    }
}