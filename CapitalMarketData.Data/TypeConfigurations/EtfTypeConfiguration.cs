using CapitalMarketData.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalMarketData.Data.TypeConfigurations;

public class EtfTypeConfiguration : IEntityTypeConfiguration<ETF>
{
    public void Configure(EntityTypeBuilder<ETF> builder)
    {
    }
}
