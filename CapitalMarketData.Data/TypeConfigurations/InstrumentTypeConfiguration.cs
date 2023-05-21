using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class InstrumentTypeConfiguration : IEntityTypeConfiguration<Instrument>
{
    private const string TableName = "Instruments";

    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.InsCode)
            .HasMaxLength(32);

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Name)
            .HasMaxLength(64);

        builder.Property(x => x.Board);

        builder.Property(x => x.Industry);
    }
}
