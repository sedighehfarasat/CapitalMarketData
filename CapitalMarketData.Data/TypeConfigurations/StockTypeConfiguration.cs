using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class StockTypeConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.Property(x => x.Board);
    }
}
