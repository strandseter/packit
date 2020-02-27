using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class ListUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingLists_Items_ItemId",
                table: "PackingLists");

            migrationBuilder.DropIndex(
                name: "IX_PackingLists_ItemId",
                table: "PackingLists");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "PackingLists");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SharedPackingLists",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackingListId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedPackingLists_UserId",
                table: "SharedPackingLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PackingListId",
                table: "Items",
                column: "PackingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PackingLists_PackingListId",
                table: "Items",
                column: "PackingListId",
                principalTable: "PackingLists",
                principalColumn: "PackingListId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedPackingLists_Users_UserId",
                table: "SharedPackingLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PackingLists_PackingListId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedPackingLists_Users_UserId",
                table: "SharedPackingLists");

            migrationBuilder.DropIndex(
                name: "IX_SharedPackingLists_UserId",
                table: "SharedPackingLists");

            migrationBuilder.DropIndex(
                name: "IX_Items_PackingListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SharedPackingLists");

            migrationBuilder.DropColumn(
                name: "PackingListId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "PackingLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PackingLists_ItemId",
                table: "PackingLists",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingLists_Items_ItemId",
                table: "PackingLists",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
