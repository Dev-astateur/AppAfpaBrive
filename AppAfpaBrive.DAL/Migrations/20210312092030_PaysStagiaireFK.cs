using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class PaysStagiaireFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaire_Idpays2",
                table: "Beneficiaire",
                column: "Idpays2");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaire_Pays",
                table: "Beneficiaire",
                column: "Idpays2",
                principalTable: "Pays",
                principalColumn: "IDPays2",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaire_Pays",
                table: "Beneficiaire");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaire_Idpays2",
                table: "Beneficiaire");

            
        }
    }
}
