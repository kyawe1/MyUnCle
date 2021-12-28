using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "682bf281-5cf6-432f-8aaa-58672659709f", "a4aec478-9336-4342-9262-08d10bcdce16", "Admin", "ADMIN" },
                    { "ff6aa31e-e460-4f8d-b3c2-0d4306edfdff", "39909018-3cf5-4043-8418-63505e3aca0c", "Shopkeeper", "SHOPKEEPER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "682bf281-5cf6-432f-8aaa-58672659709f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff6aa31e-e460-4f8d-b3c2-0d4306edfdff");
        }
    }
}
