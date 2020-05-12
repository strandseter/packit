using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Checks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checks_BackpackId",
                table: "Checks",
                column: "BackpackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Backpacks_BackpackId",
                table: "Checks",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Backpacks_BackpackId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_BackpackId",
                table: "Checks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Checks");
        }
    }
}
