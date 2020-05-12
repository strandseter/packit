using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.DataAccess.Migrations
{
    public partial class dfgdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Checks",
                newName: "CheckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckId",
                table: "Checks",
                newName: "Id");
        }
    }
}
