using Microsoft.EntityFrameworkCore.Migrations;

namespace CAPPWebApi.Migrations
{
    public partial class addfiled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayType",
                table: "TodayWeathers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsOverTime",
                table: "TodayWeathers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayType",
                table: "TodayWeathers");

            migrationBuilder.DropColumn(
                name: "IsOverTime",
                table: "TodayWeathers");
        }
    }
}
