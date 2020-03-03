using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPasword",
                table: "Users",
                newName: "HashedPassword");

            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "Trips",
                newName: "ImageStringName");

            migrationBuilder.RenameColumn(
                name: "ImageFilePath",
                table: "Items",
                newName: "ImageStringName");

            migrationBuilder.AddColumn<string>(
                name: "ImageStringName",
                table: "Backpacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageStringName",
                table: "Backpacks");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "HashedPasword");

            migrationBuilder.RenameColumn(
                name: "ImageStringName",
                table: "Trips",
                newName: "ImageFileName");

            migrationBuilder.RenameColumn(
                name: "ImageStringName",
                table: "Items",
                newName: "ImageFilePath");
        }
    }
}
