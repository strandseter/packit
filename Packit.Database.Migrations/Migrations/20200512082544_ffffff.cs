using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class ffffff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Items_ItemId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "ItemmId",
                table: "Checks");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Checks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Items_ItemId",
                table: "Checks",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Items_ItemId",
                table: "Checks");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Checks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ItemmId",
                table: "Checks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Items_ItemId",
                table: "Checks",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
