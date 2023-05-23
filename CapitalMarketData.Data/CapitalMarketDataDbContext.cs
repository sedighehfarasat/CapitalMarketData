using System.Reflection;
using Microsoft.EntityFrameworkCore;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data;

public class CapitalMarketDataDbContext : DbContext
{
    public CapitalMarketDataDbContext(DbContextOptions<CapitalMarketDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<Stock> Stocks { get; set; }

    public DbSet<TradingData> TradingData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}