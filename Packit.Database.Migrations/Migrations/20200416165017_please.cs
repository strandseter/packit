using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class please : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SharedBackpacks_UserId",
                table: "SharedBackpacks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBackpacks_Users_UserId",
                table: "SharedBackpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedBackpacks_Users_UserId",
                table: "SharedBackpacks");

            migrationBuilder.DropIndex(
                name: "IX_SharedBackpacks_UserId",
                table: "SharedBackpacks");
        }
    }
}
