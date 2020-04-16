using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class DGDFGH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.RenameColumn(
                name: "df",
                table: "Items",
                newName: "IGPHKJ");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.RenameColumn(
                name: "IGPHKJ",
                table: "Items",
                newName: "df");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
