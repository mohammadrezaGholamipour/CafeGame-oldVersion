using CafeGameApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeGameApi.Context.Configs;

public class FoodConfig : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany<BillFood>(x => x.BillFoods)
            .WithOne(x => x.Food)
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}