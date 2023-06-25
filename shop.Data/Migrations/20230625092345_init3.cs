using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Wallet");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Wallet");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
