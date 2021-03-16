using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class PeeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateIndex(
                name: "IX_Pee_IdEntreprise",
                table: "Pee",
                column: "IdEntreprise");

            migrationBuilder.AddForeignKey(
                name: "FK_Pee_Entreprise",
                table: "Pee",
                column: "IdEntreprise",
                principalTable: "Entreprise",
                principalColumn: "IdEntreprise",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pee_Entreprise",
                table: "Pee");

            migrationBuilder.DropIndex(
                name: "IX_Pee_IdEntreprise",
                table: "Pee");

           
        }
    }
}
