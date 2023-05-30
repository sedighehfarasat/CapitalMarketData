using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class EtfTypeConfiguration : IEntityTypeConfiguration<ETF>
{
    private const string TableName = "ETFs";

    public void Configure(EntityTypeBuilder<ETF> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(0);

        builder.Property(x => x.InsCode)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(1);

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(2);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64)
            .HasColumnOrder(3);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnOrder(4);
    }
}
