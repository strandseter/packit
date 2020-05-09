using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class blaaah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
