using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class BackPackUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PackingLists_PackingListId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "SharedPackingLists");

            migrationBuilder.DropTable(
                name: "PackingLists");

            migrationBuilder.DropIndex(
                name: "IX_Items_PackingListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PackingListId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "Backpacks",
                columns: table => new
                {
                    BackpackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    TripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backpacks", x => x.BackpackId);
                    table.ForeignKey(
                        name: "FK_Backpacks_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Backpacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemBackpack",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    BackpackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBackpack", x => new { x.ItemId, x.BackpackId });
                    table.ForeignKey(
                        name: "FK_ItemBackpack_Backpacks_BackpackId",
                        column: x => x.BackpackId,
                        principalTable: "Backpacks",
                        principalColumn: "BackpackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemBackpack_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedBackpacks",
                columns: table => new
                {
                    SharedBackpackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BackpackId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedBackpacks", x => x.SharedBackpackId);
                    table.ForeignKey(
                        name: "FK_SharedBackpacks_Backpacks_BackpackId",
                        column: x => x.BackpackId,
                        principalTable: "Backpacks",
                        principalColumn: "BackpackId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SharedBackpacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Backpacks_TripId",
                table: "Backpacks",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Backpacks_UserId",
                table: "Backpacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBackpack_BackpackId",
                table: "ItemBackpack",
                column: "BackpackId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedBackpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedBackpacks_UserId",
                table: "SharedBackpacks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBackpack");

            migrationBuilder.DropTable(
                name: "SharedBackpacks");

            migrationBuilder.DropTable(
                name: "Backpacks");

            migrationBuilder.AddColumn<int>(
                name: "PackingListId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackingLists",
                columns: table => new
                {
                    PackingListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    TripId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingLists", x => x.PackingListId);
                    table.ForeignKey(
                        name: "FK_PackingLists_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackingLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SharedPackingLists",
                columns: table => new
                {
                    SharedPackingListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackingListId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedPackingLists", x => x.SharedPackingListId);
                    table.ForeignKey(
                        name: "FK_SharedPackingLists_PackingLists_PackingListId",
                        column: x => x.PackingListId,
                        principalTable: "PackingLists",
                        principalColumn: "PackingListId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SharedPackingLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_PackingListId",
                table: "Items",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingLists_TripId",
                table: "PackingLists",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingLists_UserId",
                table: "PackingLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedPackingLists_PackingListId",
                table: "SharedPackingLists",
                column: "PackingListId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedPackingLists_UserId",
                table: "SharedPackingLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PackingLists_PackingListId",
                table: "Items",
                column: "PackingListId",
                principalTable: "PackingLists",
                principalColumn: "PackingListId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
