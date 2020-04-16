using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class EfCoreIsShiter4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hei",
                table: "Items",
                newName: "halla");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Backpacks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "halla",
                table: "Items",
                newName: "hei");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Trips",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Backpacks",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
