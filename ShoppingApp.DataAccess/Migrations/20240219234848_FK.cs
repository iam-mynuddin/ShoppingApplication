using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CategoryId_FK",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_tblCategories_CategoryId_FK",
                table: "Products",
                column: "CategoryId_FK",
                principalTable: "tblCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_tblCategories_CategoryId_FK",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId_FK",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId_FK",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "CategoryId",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_tblCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "tblCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
