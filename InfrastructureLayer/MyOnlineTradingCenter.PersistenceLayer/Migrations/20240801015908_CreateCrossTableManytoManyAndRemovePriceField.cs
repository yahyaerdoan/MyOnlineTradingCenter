using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineTradingCenter.PersistenceLayer.Migrations
{
    public partial class CreateCrossTableManytoManyAndRemovePriceField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "UploadedFiles");

            migrationBuilder.CreateTable(
                name: "ImageFileProduct",
                columns: table => new
                {
                    ImageFilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFileProduct", x => new { x.ImageFilesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ImageFileProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageFileProduct_UploadedFiles_ImageFilesId",
                        column: x => x.ImageFilesId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageFileProduct_ProductsId",
                table: "ImageFileProduct",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageFileProduct");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UploadedFiles",
                type: "numeric",
                nullable: true);
        }
    }
}
