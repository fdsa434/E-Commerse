using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecpmmerce.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class pro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_brands_brandid",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_types_Typeid",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "brandid",
                table: "products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "Typeid",
                table: "products",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_Typeid",
                table: "products",
                newName: "IX_products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_brandid",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_types_TypeId",
                table: "products",
                column: "TypeId",
                principalTable: "types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_brands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_types_TypeId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "products",
                newName: "Typeid");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "products",
                newName: "brandid");

            migrationBuilder.RenameIndex(
                name: "IX_products_TypeId",
                table: "products",
                newName: "IX_products_Typeid");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "products",
                newName: "IX_products_brandid");

            migrationBuilder.AddForeignKey(
                name: "FK_products_brands_brandid",
                table: "products",
                column: "brandid",
                principalTable: "brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_types_Typeid",
                table: "products",
                column: "Typeid",
                principalTable: "types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
