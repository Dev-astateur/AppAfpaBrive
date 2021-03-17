using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class InsertionsTroisMois : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsertionTroisMois",
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
                    table.PrimaryKey("PK_InsertionTroisMois", x => new { x.IdEtablissement, x.IdOffreFormation, x.Annee });
                    table.ForeignKey(
                        name: "FK_InsertionTroisMois_Etablissement_IdEtablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsertionTroisMois_ProduitFormation_IdOffreFormation",
                        column: x => x.IdOffreFormation,
                        principalTable: "ProduitFormation",
                        principalColumn: "CodeProduitFormation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsertionTroisMois_IdOffreFormation",
                table: "InsertionTroisMois",
                column: "IdOffreFormation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsertionTroisMois");
        }
    }
}
