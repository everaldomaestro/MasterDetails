using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterDetails.Migrations
{
    public partial class qtd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Qtd",
                table: "Details",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qtd",
                table: "Details");
        }
    }
}
