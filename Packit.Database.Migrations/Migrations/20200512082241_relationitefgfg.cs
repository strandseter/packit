using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class relationitefgfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemmId",
                table: "Checks");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "Checks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
