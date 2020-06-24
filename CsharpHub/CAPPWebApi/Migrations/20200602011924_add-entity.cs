using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAPPWebApi.Migrations
{
    public partial class addentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodayWeathers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    Weather = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Wind = table.Column<string>(nullable: true),
                    Sky = table.Column<string>(nullable: true),
                    Today = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodayWeathers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodayWeathers");
        }
    }
}
