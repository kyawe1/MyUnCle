using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
