using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class AjoutHeureDansTableEvenement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Professionnel_TitreCivilite",
            //    table: "Professionnel");

            migrationBuilder.AddColumn<string>(
                name: "Heure",
                table: "Evenement",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "CodeTitreCiviliteProfessionnel",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel",
                principalTable: "TitreCivilite",
                principalColumn: "CodeTitreCivilite",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "CodeTitreCiviliteProfessionnel",
            //    table: "Professionnel");

            migrationBuilder.DropColumn(
                name: "Heure",
                table: "Evenement");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel",
                principalTable: "TitreCivilite",
                principalColumn: "CodeTitreCivilite",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
