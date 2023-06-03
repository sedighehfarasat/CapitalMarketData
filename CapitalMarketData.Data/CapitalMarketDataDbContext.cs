using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CapitalMarketData.Data;

public class CapitalMarketDataDbContext : DbContext
{
    public CapitalMarketDataDbContext(DbContextOptions<CapitalMarketDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<ETF> ETFs { get; set; }
    public DbSet<TradingData> TradingData { get; set; }
    public DbSet<IndiInstiTradingData> IndiInstiTradingData { get; set; }
    public DbSet<NAV> NAVs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseCollation("Persian_100_CI_AI_KS_WS_SC");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}