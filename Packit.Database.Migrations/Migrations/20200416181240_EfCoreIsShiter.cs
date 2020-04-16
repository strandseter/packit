using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class EfCoreIsShiter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "blah",
                table: "Items",
                newName: "hei");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "hei",
                table: "Items",
                newName: "blah");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
