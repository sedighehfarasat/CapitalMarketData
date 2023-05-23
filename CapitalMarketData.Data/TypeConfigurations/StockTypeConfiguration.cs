using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class StockTypeConfiguration : IEntityTypeConfiguration<Stock>
{
    private const string TableName = "Stocks";

    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(0);

        builder.Property(x => x.InsCode)
            .HasMaxLength(32)
            .HasColumnOrder(1);

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnOrder(2);

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .HasColumnOrder(3);

        builder.Property(x => x.Board)
            .HasColumnOrder(4);

        builder.Property(x => x.Industry)
            .HasColumnOrder(5);
    }
}
