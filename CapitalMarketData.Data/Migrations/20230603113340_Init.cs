using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalMarketData.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    InsCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Subsector = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Board = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualInstitutionalTradingData",
                columns: table => new
                {
                    InstrumentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndividualNumberOfTrades_BuySide = table.Column<int>(type: "int", nullable: true),
                    IndividualNumberOfTrades_SellSide = table.Column<int>(type: "int", nullable: true),
                    IndividualTradingVolume_BuySide = table.Column<long>(type: "bigint", nullable: true),
                    IndividualTradingVolume_SellSide = table.Column<long>(type: "bigint", nullable: true),
                    InstitutionalNumberOfTrades_BuySide = table.Column<int>(type: "int", nullable: true),
                    InstitutionalNumberOfTrades_SellSide = table.Column<int>(type: "int", nullable: true),
                    InstitutionalTradingVolume_BuySide = table.Column<long>(type: "bigint", nullable: true),
                    InstitutionalTradingVolume_SellSide = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualInstitutionalTradingData", x => new { x.InstrumentId, x.Date });
                    table.ForeignKey(
                        name: "FK_IndividualInstitutionalTradingData_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NAVs",
                columns: table => new
                {
                    InstrumentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NetAssetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAVs", x => new { x.InstrumentId, x.Date });
                    table.ForeignKey(
                        name: "FK_NAVs_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradingData",
                columns: table => new
                {
                    InstrumentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PreviousClosingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpperBoundPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LowerBoundPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NumberOfTrades = table.Column<int>(type: "int", nullable: true),
                    TradingValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TradingVolume = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingData", x => new { x.InstrumentId, x.Date });
                    table.ForeignKey(
                        name: "FK_TradingData_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualInstitutionalTradingData_InstrumentId",
                table: "IndividualInstitutionalTradingData",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_NAVs_InstrumentId",
                table: "NAVs",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingData_InstrumentId",
                table: "TradingData",
                column: "InstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualInstitutionalTradingData");

            migrationBuilder.DropTable(
                name: "NAVs");

            migrationBuilder.DropTable(
                name: "TradingData");

            migrationBuilder.DropTable(
                name: "Instruments");
        }
    }
}
