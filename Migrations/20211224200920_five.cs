using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class five : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "dumblingTypes",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "dumblingTypes",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
