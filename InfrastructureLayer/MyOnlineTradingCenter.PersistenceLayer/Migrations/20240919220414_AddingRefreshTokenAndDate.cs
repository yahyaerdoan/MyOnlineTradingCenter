using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineTradingCenter.PersistenceLayer.Migrations
{
    public partial class AddingRefreshTokenAndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenExpirationDate",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDate",
                table: "AspNetUsers");
        }
    }
}
