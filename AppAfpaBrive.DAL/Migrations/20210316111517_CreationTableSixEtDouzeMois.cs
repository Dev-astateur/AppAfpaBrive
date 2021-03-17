using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class CreationTableSixEtDouzeMois : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsertionTroisMois_Etablissement_IdEtablissement",
                table: "InsertionTroisMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionTroisMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionTroisMois",
                table: "InsertionTroisMois");

            migrationBuilder.RenameTable(
                name: "InsertionTroisMois",
                newName: "InsertionsTroisMois");

            migrationBuilder.RenameIndex(
                name: "IX_InsertionTroisMois_IdOffreFormation",
                table: "InsertionsTroisMois",
                newName: "IX_InsertionsTroisMois_IdOffreFormation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "Annee" });

            migrationBuilder.CreateTable(
                name: "InsertionsDouzeMois",
                columns: table => new
                {
                    IdEtablissement = table.Column<string>(type: "char(5)", fixedLength: true, maxLength: 5, nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    TotalReponse = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Cdi = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Cdd = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Alternance = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SansEmploie = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Autres = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsertionsDouzeMois", x => new { x.IdEtablissement, x.IdOffreFormation, x.Annee });
                    table.ForeignKey(
                        name: "FK_InsertionsDouzeMois_Etablissement_IdEtablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsertionsDouzeMois_ProduitFormation_IdOffreFormation",
                        column: x => x.IdOffreFormation,
                        principalTable: "ProduitFormation",
                        principalColumn: "CodeProduitFormation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsertionsSixMois",
                columns: table => new
                {
                    IdEtablissement = table.Column<string>(type: "char(5)", fixedLength: true, maxLength: 5, nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    TotalReponse = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Cdi = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Cdd = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Alternance = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SansEmploie = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Autres = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsertionsSixMois", x => new { x.IdEtablissement, x.IdOffreFormation, x.Annee });
                    table.ForeignKey(
                        name: "FK_InsertionsSixMois_Etablissement_IdEtablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsertionsSixMois_ProduitFormation_IdOffreFormation",
                        column: x => x.IdOffreFormation,
                        principalTable: "ProduitFormation",
                        principalColumn: "CodeProduitFormation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsDouzeMois_IdOffreFormation",
                table: "InsertionsDouzeMois",
                column: "IdOffreFormation");

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsSixMois_IdOffreFormation",
                table: "InsertionsSixMois",
                column: "IdOffreFormation");

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsTroisMois_Etablissement_IdEtablissement",
                table: "InsertionsTroisMois",
                column: "IdEtablissement",
                principalTable: "Etablissement",
                principalColumn: "IdEtablissement",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsTroisMois",
                column: "IdOffreFormation",
                principalTable: "ProduitFormation",
                principalColumn: "CodeProduitFormation",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsTroisMois_Etablissement_IdEtablissement",
                table: "InsertionsTroisMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsTroisMois");

            migrationBuilder.DropTable(
                name: "InsertionsDouzeMois");

            migrationBuilder.DropTable(
                name: "InsertionsSixMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois");

            migrationBuilder.RenameTable(
                name: "InsertionsTroisMois",
                newName: "InsertionTroisMois");

            migrationBuilder.RenameIndex(
                name: "IX_InsertionsTroisMois_IdOffreFormation",
                table: "InsertionTroisMois",
                newName: "IX_InsertionTroisMois_IdOffreFormation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionTroisMois",
                table: "InsertionTroisMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "Annee" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionTroisMois_Etablissement_IdEtablissement",
                table: "InsertionTroisMois",
                column: "IdEtablissement",
                principalTable: "Etablissement",
                principalColumn: "IdEtablissement",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionTroisMois",
                column: "IdOffreFormation",
                principalTable: "ProduitFormation",
                principalColumn: "CodeProduitFormation",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
