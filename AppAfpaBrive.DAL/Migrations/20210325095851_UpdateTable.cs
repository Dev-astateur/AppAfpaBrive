using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsDouzeMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsDouzeMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsSixMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsSixMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsTroisMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsTroisMois_IdOffreFormation",
                table: "InsertionsTroisMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsSixMois_IdOffreFormation",
                table: "InsertionsSixMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsDouzeMois_IdOffreFormation",
                table: "InsertionsDouzeMois");

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsTroisMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsTroisMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" });

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsSixMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsSixMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" });

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsDouzeMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsDouzeMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsDouzeMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsDouzeMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" },
                principalTable: "OffreFormation",
                principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsSixMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsSixMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" },
                principalTable: "OffreFormation",
                principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsTroisMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsTroisMois",
                columns: new[] { "IdOffreFormation", "IdEtablissement" },
                principalTable: "OffreFormation",
                principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsDouzeMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsDouzeMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsSixMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsSixMois");

            migrationBuilder.DropForeignKey(
                name: "FK_InsertionsTroisMois_OffreFormation_IdOffreFormation_IdEtablissement",
                table: "InsertionsTroisMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsTroisMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsTroisMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsSixMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsSixMois");

            migrationBuilder.DropIndex(
                name: "IX_InsertionsDouzeMois_IdOffreFormation_IdEtablissement",
                table: "InsertionsDouzeMois");

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsTroisMois_IdOffreFormation",
                table: "InsertionsTroisMois",
                column: "IdOffreFormation");

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsSixMois_IdOffreFormation",
                table: "InsertionsSixMois",
                column: "IdOffreFormation");

            migrationBuilder.CreateIndex(
                name: "IX_InsertionsDouzeMois_IdOffreFormation",
                table: "InsertionsDouzeMois",
                column: "IdOffreFormation");

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsDouzeMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsDouzeMois",
                column: "IdOffreFormation",
                principalTable: "ProduitFormation",
                principalColumn: "CodeProduitFormation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsSixMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsSixMois",
                column: "IdOffreFormation",
                principalTable: "ProduitFormation",
                principalColumn: "CodeProduitFormation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsertionsTroisMois_ProduitFormation_IdOffreFormation",
                table: "InsertionsTroisMois",
                column: "IdOffreFormation",
                principalTable: "ProduitFormation",
                principalColumn: "CodeProduitFormation",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
