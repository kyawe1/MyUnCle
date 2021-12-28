using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UncleApp.Migrations
{
    public partial class mm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_dumblingTypes_TypeId",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_TypeId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "orderItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_dumblingid",
                table: "orderItems",
                column: "dumblingid");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_dumblingTypes_dumblingid",
                table: "orderItems",
                column: "dumblingid",
                principalTable: "dumblingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_dumblingTypes_dumblingid",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_dumblingid",
                table: "orderItems");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "orderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_TypeId",
                table: "orderItems",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_dumblingTypes_TypeId",
                table: "orderItems",
                column: "TypeId",
                principalTable: "dumblingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
