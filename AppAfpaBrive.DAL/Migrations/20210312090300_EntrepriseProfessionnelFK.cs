using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class EntrepriseProfessionnelFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_Professionnel_IdProfessionnel",
                table: "Entreprise_Professionnel",
                column: "IdProfessionnel");

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Professionnel_Entreprise_IdEntreprise",
                table: "Entreprise_Professionnel",
                column: "IdEntreprise",
                principalTable: "Entreprise",
                principalColumn: "IdEntreprise",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Professionnel_Professionnel_IdProfessionnel",
                table: "Entreprise_Professionnel",
                column: "IdProfessionnel",
                principalTable: "Professionnel",
                principalColumn: "IdProfessionnel",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Professionnel_Entreprise_IdEntreprise",
                table: "Entreprise_Professionnel");

            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Professionnel_Professionnel_IdProfessionnel",
                table: "Entreprise_Professionnel");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_Professionnel_IdProfessionnel",
                table: "Entreprise_Professionnel");
        }
    }
}
