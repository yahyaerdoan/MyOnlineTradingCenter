using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOnlineTradingCenter.PersistenceLayer.Migrations
{
    public partial class AddDateTimeForRefreshTokenExpirationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""AspNetUsers"" 
                ALTER COLUMN ""RefreshTokenExpirationDate"" 
                TYPE timestamp with time zone 
                USING ""RefreshTokenExpirationDate""::timestamp with time zone;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenExpirationDate",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
