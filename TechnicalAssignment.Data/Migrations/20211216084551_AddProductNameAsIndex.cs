using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechnicalAssignment.Data.Migrations
{
    public partial class AddProductNameAsIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Product");
        }
    }
}
