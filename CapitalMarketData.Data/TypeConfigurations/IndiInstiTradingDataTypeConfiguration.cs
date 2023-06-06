using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class IndiInstiTradingDataTypeConfiguration : IEntityTypeConfiguration<IndiInstiTradingData>
{
    private const string TableName = "IndividualInstitutionalTradingData";

    public void Configure(EntityTypeBuilder<IndiInstiTradingData> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => new { x.InstrumentId, x.Date });

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.InstrumentId)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.IndividualNumberOfTrades_BuySide)
            .HasColumnType("int"); 
        
        builder.Property(x => x.IndividualNumberOfTrades_SellSide)
            .HasColumnType("int");

        builder.Property(x => x.IndividualTradingVolume_BuySide)
            .HasColumnType("bigint");

        builder.Property(x => x.IndividualTradingVolume_SellSide)
            .HasColumnType("bigint");

        builder.Property(x => x.InstitutionalNumberOfTrades_BuySide)
            .HasColumnType("int");

        builder.Property(x => x.InstitutionalNumberOfTrades_SellSide)
            .HasColumnType("int");

        builder.Property(x => x.InstitutionalTradingVolume_BuySide)
            .HasColumnType("bigint");

        builder.Property(x => x.InstitutionalTradingVolume_SellSide)
            .HasColumnType("bigint");

        builder.HasOne(x => x.Instrument)
            .WithMany(x => x.IndiInstiTradingData)
            .HasForeignKey(x => x.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.InstrumentId);
    }
}
