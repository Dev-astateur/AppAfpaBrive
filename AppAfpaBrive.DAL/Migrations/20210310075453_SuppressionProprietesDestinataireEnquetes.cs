using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class SuppressionProprietesDestinataireEnquetes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtapeQuestionnaire",
                table: "DestinataireEnquete");

            migrationBuilder.DropColumn(
                name: "EtatTraitementQuestionnaire",
                table: "DestinataireEnquete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtapeQuestionnaire",
                table: "DestinataireEnquete",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EtatTraitementQuestionnaire",
                table: "DestinataireEnquete",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
