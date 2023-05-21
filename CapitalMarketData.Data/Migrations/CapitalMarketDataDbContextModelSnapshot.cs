﻿// <auto-generated />
using System;
using CapitalMarketData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CapitalMarketData.Data.Migrations
{
    [DbContext(typeof(CapitalMarketDataDbContext))]
    partial class CapitalMarketDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CapitalMarketData.Entities.Entities.Instrument", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("Board")
                        .HasColumnType("int");

                    b.Property<int?>("Industry")
                        .HasColumnType("int");

                    b.Property<string>("InsCode")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Instruments", (string)null);
                });

            modelBuilder.Entity("CapitalMarketData.Entities.Entities.TradingData", b =>
                {
                    b.Property<string>("InstrumentId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("ClosingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("HighestPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LastPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LowerBoundPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("LowestPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("NumberOfTrades")
                        .HasColumnType("int");

                    b.Property<decimal?>("OpeningPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PreviousClosingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("TradingValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("TradingVolume")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("UpperBoundPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InstrumentId", "Date");

                    b.HasIndex("InstrumentId");

                    b.ToTable("TradingData", (string)null);
                });

            modelBuilder.Entity("CapitalMarketData.Entities.Entities.TradingData", b =>
                {
                    b.HasOne("CapitalMarketData.Entities.Entities.Instrument", "Instrument")
                        .WithMany("TradingData")
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrument");
                });

            modelBuilder.Entity("CapitalMarketData.Entities.Entities.Instrument", b =>
                {
                    b.Navigation("TradingData");
                });
#pragma warning restore 612, 618
        }
    }
}
