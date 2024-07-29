using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineTradingCenter.PersistenceLayer.Migrations
{
    public partial class AddingStoragePropery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "UploadedFiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Storage",
                table: "UploadedFiles");
        }
    }
}
