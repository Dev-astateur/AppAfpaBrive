using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class FK_ProfessionnelTitreCivilite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                     

            migrationBuilder.CreateIndex(
                name: "IX_Professionnel_TitreCiviliteNavigationCodeTitreCivilite",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel");
              

            migrationBuilder.AddForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel",
                principalTable: "TitreCivilite",
                principalColumn: "CodeTitreCivilite",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel");

            migrationBuilder.DropIndex(
                name: "IX_Professionnel_TitreCiviliteNavigationCodeTitreCivilite",
                table: "Professionnel");


        }
    }
}
