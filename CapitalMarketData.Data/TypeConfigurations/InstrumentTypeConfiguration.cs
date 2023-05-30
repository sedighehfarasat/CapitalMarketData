using CapitalMarketData.Entities.Entities;
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
