using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class AddReponduInDestinataireEnquete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Repondu",
                table: "DestinataireEnquete",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repondu",
                table: "DestinataireEnquete");
        }
    }
}
