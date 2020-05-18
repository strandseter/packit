using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class checkupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Backpacks_BackpackId",
                table: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Checks_BackpackId",
                table: "Checks");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Checks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Checks");

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
    }
}
