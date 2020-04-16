using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class ggggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Items");
        }
    }
}
