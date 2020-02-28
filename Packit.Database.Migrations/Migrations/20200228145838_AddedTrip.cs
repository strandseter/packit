using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class AddedTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Trips_TripId",
                table: "Backpacks");

            migrationBuilder.DropIndex(
                name: "IX_Backpacks_TripId",
                table: "Backpacks");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Backpacks");

            migrationBuilder.CreateTable(
                name: "BackpackTrip",
                columns: table => new
                {
                    BackpackId = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackpackTrip", x => new { x.BackpackId, x.TripId });
                    table.ForeignKey(
                        name: "FK_BackpackTrip_Backpacks_BackpackId",
                        column: x => x.BackpackId,
                        principalTable: "Backpacks",
                        principalColumn: "BackpackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackpackTrip_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackpackTrip_TripId",
                table: "BackpackTrip",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackpackTrip");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Backpacks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Backpacks_TripId",
                table: "Backpacks",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Trips_TripId",
                table: "Backpacks",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
