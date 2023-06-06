using CapitalMarketData.Entities.Entities;
using CapitalMarketData.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CapitalMarketData.Data.TypeConfigurations;

public class InstrumentTypeConfiguration : IEntityTypeConfiguration<Instrument>
{
    private const string TableName = "Instruments";

    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder.ToTable(TableName);

        builder.HasDiscriminator<string>("Discriminator");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.InsCode)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Ticker)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x => x.Type)
            .HasConversion(x => x.ToString(), x => (InstrumentType)Enum.Parse(typeof(InstrumentType), x))
            .HasMaxLength(256);

        builder.Property(x => x.Sector)
            .HasConversion(x => x.ToString(), x => (Sector)Enum.Parse(typeof(Sector), x))
            .HasMaxLength(256);

        builder.Property(x => x.Subsector)
            .HasConversion(x => x.ToString(), x => (Subsector)Enum.Parse(typeof(Subsector), x))
            .HasMaxLength(256);
    }
}
