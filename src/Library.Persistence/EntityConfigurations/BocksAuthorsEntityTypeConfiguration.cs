using Library.Core.Domain.Bocks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.EntityConfigurations;

internal class BocksAuthorsEntityTypeConfiguration : IEntityTypeConfiguration<BockAuthor>
{
    public void Configure(EntityTypeBuilder<BockAuthor> builder)
    {
        builder.HasKey(ba => new { ba.BockId, ba.AuthorId });

        builder.HasOne(ba => ba.Bock)
            .WithMany(b => b.BocksAuthors)
            .HasForeignKey(ba => ba.BockId);

        builder.HasOne(ba => ba.Author)
            .WithMany(a => a.BocksAuthors)
            .HasForeignKey(ba => ba.AuthorId);
    }
}