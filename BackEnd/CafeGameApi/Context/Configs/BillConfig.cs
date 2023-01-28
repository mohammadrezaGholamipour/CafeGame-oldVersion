using CafeGameApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeGameApi.Context.Configs;

public class BillConfig:IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FinalCost)
            .IsRequired(false);

        builder.HasOne(x => x.System)
            .WithMany(x => x.Bills)
            .HasForeignKey(x => x.SystemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.BillFoods)
            .WithOne(x => x.Bill)
            .HasForeignKey(x => x.BillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}