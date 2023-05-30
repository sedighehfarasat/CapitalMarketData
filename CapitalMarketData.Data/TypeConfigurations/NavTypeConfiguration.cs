using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class NavTypeConfiguration : IEntityTypeConfiguration<NAV>
{
    private const string TableName = "NAVs";

    public void Configure(EntityTypeBuilder<NAV> builder)
    {
        builder.ToTable(TableName);

        builder.HasKey(x => new { x.InstrumentId, x.Date });

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.InstrumentId)
            .IsRequired()
            .HasMaxLength(32);    

        builder.Property(x => x.NetAssetValue)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.ETF)
                .WithMany(x => x.NAV)
                .HasForeignKey(x => x.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.InstrumentId);
    }
}
