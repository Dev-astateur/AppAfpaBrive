using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class AddColEnLienFormationInInsertionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsSixMois",
                table: "InsertionsSixMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsDouzeMois",
                table: "InsertionsDouzeMois");

            migrationBuilder.AddColumn<bool>(
                name: "EnLienAvecFormation",
                table: "InsertionsTroisMois",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnLienAvecFormation",
                table: "InsertionsSixMois",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnLienAvecFormation",
                table: "InsertionsDouzeMois",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "EnLienAvecFormation", "Annee" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsSixMois",
                table: "InsertionsSixMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "EnLienAvecFormation", "Annee" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsDouzeMois",
                table: "InsertionsDouzeMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "EnLienAvecFormation", "Annee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsSixMois",
                table: "InsertionsSixMois");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsertionsDouzeMois",
                table: "InsertionsDouzeMois");

            migrationBuilder.DropColumn(
                name: "EnLienAvecFormation",
                table: "InsertionsTroisMois");

            migrationBuilder.DropColumn(
                name: "EnLienAvecFormation",
                table: "InsertionsSixMois");

            migrationBuilder.DropColumn(
                name: "EnLienAvecFormation",
                table: "InsertionsDouzeMois");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsTroisMois",
                table: "InsertionsTroisMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "Annee" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsSixMois",
                table: "InsertionsSixMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "Annee" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsertionsDouzeMois",
                table: "InsertionsDouzeMois",
                columns: new[] { "IdEtablissement", "IdOffreFormation", "Annee" });

           
        }
    }
}
