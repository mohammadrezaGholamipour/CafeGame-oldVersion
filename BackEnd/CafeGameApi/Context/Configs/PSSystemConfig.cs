using CafeGameApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeGameApi.Context.Configs;

public class PSSystemConfig : IEntityTypeConfiguration<PSSystem>
{
    public void Configure(EntityTypeBuilder<PSSystem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasMany<Bill>(x => x.Bills)
            .WithOne(x => x.System)
            .HasForeignKey(x => x.SystemId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}