using CafeGameApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeGameApi.Context.Configs;

public class HourRateConfig : IEntityTypeConfiguration<HourRate>
{
    public void Configure(EntityTypeBuilder<HourRate> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}