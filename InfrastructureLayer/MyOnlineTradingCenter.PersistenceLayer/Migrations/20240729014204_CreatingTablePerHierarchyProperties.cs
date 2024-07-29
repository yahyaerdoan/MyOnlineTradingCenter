using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineTradingCenter.PersistenceLayer.Migrations
{
    public partial class CreatingTablePerHierarchyProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UploadedFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "UploadedFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "UploadedFiles",
                type: "numeric",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "UploadedFiles");
        }
    }
}
