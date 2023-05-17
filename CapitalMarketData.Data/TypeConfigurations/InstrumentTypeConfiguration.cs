﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CapitalMarketData.Entities.Entities;

namespace CapitalMarketData.Data.TypeConfigurations;

public class InstrumentTypeConfiguration : IEntityTypeConfiguration<Instrument>
{
    private const string TableName = "Instruments";
    private const string FilePath = @"F:\Algorithmic Trading\Capital Market Data\Documents\ISIN.csv";

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

        using (StreamReader textReader = File.OpenText(FilePath))
        {
            while (!textReader.EndOfStream)
            {
                var line = textReader.ReadLine()!;
                if (line.Contains("IR"))
                {
                    builder.HasData(new Instrument { Id = line.Split(',')[1], Ticker = line.Split(',')[0] });
                }
            }
        }
    }
}
