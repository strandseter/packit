using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class AddedOneToOneBackpack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.DropIndex(
                name: "IX_SharedBackpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.AlterColumn<int>(
                name: "BackpackId",
                table: "SharedBackpacks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedBackpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.DropIndex(
                name: "IX_SharedBackpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.AlterColumn<int>(
                name: "BackpackId",
                table: "SharedBackpacks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_SharedBackpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
