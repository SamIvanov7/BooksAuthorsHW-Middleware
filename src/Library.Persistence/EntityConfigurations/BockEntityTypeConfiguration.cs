using Library.Core.Domain.Bocks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.EntityConfigurations;

public class BockEntityTypeConfiguration : IEntityTypeConfiguration<Bock>
{
    public void Configure(EntityTypeBuilder<Bock> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(b => b.BocksAuthors)
            .WithOne(ba => ba.Bock)
            .HasForeignKey(ba => ba.BockId);
    }
}