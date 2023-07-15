using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapitalMarketData.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Instruments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Board",
                table: "Instruments",
                type: "int",
                nullable: true);
        }
    }
}
