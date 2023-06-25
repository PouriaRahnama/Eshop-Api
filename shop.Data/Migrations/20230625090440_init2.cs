using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerStatusId",
                table: "Seller");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Seller",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Seller");

            migrationBuilder.AddColumn<int>(
                name: "SellerStatusId",
                table: "Seller",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
