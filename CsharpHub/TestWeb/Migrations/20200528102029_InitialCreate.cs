using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    Weath = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Wind = table.Column<string>(nullable: true),
                    WindLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherModels");
        }
    }
}
