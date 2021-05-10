using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class PeeDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pee_Entreprise_FK_Pee_Entreprise",
                table: "Pee");

            migrationBuilder.DropColumn(
                name: "FK_Pee_Entreprise",
                table: "Pee");

            migrationBuilder.RenameColumn(
                name: "Etat",
                table: "Pee",
                newName: "EtatPee");

            migrationBuilder.CreateTable(
                name: "Pee_Document",
                columns: table => new
                {
                    IdPee = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NumOrdre = table.Column<int>(type: "int", nullable: false),
                    PathDocument = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pee_Document", x => new { x.IdPee, x.NumOrdre });
                    table.ForeignKey(
                        name: "FK_Pee_Document_Pee",
                        column: x => x.IdPee,
                        principalTable: "Pee",
                        principalColumn: "IdPee",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.DropTable(
                name: "Pee_Document");

            migrationBuilder.DropIndex(
                name: "IX_Pee_IdEntreprise",
                table: "Pee");

            migrationBuilder.RenameColumn(
                name: "EtatPee",
                table: "Pee",
                newName: "Etat");

            migrationBuilder.AddColumn<int>(
                name: "FK_Pee_Entreprise",
                table: "Pee",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pee_Entreprise_FK_Pee_Entreprise",
                table: "Pee",
                column: "FK_Pee_Entreprise",
                principalTable: "Entreprise",
                principalColumn: "IdEntreprise",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
