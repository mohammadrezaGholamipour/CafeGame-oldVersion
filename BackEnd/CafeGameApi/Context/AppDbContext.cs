using CafeGameApi.Context.Configs;
using CafeGameApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeGameApi.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = System.IO.Path.Join(path, "CafeGame.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new BillConfig());
        builder.ApplyConfiguration(new FoodConfig());
        builder.ApplyConfiguration(new PSSystemConfig());
    }

    public virtual DbSet<Bill> Bills { get; set; } = null!;
    public virtual DbSet<Food> Foods { get; set; } = null!;
    public virtual DbSet<PSSystem> PSSystems { get; set; } = null!;
    public virtual DbSet<BillFood> BillFoods { get; set; } = null!;
    public virtual DbSet<HourRate> HourRates { get; set; } = null!;
}