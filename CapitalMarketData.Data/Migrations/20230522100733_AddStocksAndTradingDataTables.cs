using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalMarketData.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStocksAndTradingDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    InsCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Board = table.Column<int>(type: "int", nullable: true),
                    Industry = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
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
                        name: "FK_TradingData_Stocks_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradingData_InstrumentId",
                table: "TradingData",
                column: "InstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradingData");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
