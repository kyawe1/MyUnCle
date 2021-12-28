using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "addresses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "addresses");
        }
    }
}
