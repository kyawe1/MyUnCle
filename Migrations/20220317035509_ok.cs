using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class ok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc08328-0e64-47fb-aed7-294360e7d376");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b4917b3-0b65-4da9-a656-e507862914e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "003dac34-9714-4222-90aa-2789dd4db94d", "f7a2617a-695f-4a98-a6f2-a69ddd668990", "Shopkeeper", "SHOPKEEPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e005b2d3-ddd7-4206-b9db-952967e13e4d", "2e40a154-088e-432c-b564-606624e04c26", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "75a6f07e-acd4-4c45-82ac-bc0872119207", 0, "21fabc92-0d67-405c-9cd2-fc8551fde693", "kyawe225@gmail.com", false, false, null, null, "KYAWE225@GMAIL.COM", null, null, false, "4434aa25-e8b1-4e93-b0ab-b476d0ec408d", false, "kyawe225@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e005b2d3-ddd7-4206-b9db-952967e13e4d", "75a6f07e-acd4-4c45-82ac-bc0872119207" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "003dac34-9714-4222-90aa-2789dd4db94d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e005b2d3-ddd7-4206-b9db-952967e13e4d", "75a6f07e-acd4-4c45-82ac-bc0872119207" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e005b2d3-ddd7-4206-b9db-952967e13e4d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "75a6f07e-acd4-4c45-82ac-bc0872119207");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bc08328-0e64-47fb-aed7-294360e7d376", "16d425e1-66ba-40b8-a6c1-d6eb9338e1e6", "Shopkeeper", "SHOPKEEPER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b4917b3-0b65-4da9-a656-e507862914e9", "4100c2a5-862f-431e-9dab-64d49cda42ea", "Admin", "ADMIN" });
        }
    }
}
