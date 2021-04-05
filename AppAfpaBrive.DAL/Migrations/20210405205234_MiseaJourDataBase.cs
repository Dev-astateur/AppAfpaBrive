using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class MiseaJourDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel");

            migrationBuilder.DropTable(
                name: "Periode_Pee_Evenement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pee_Document",
                table: "Pee_Document");

            migrationBuilder.AlterColumn<decimal>(
                name: "IdPee",
                table: "Pee_Document",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,0)");

            migrationBuilder.AlterColumn<decimal>(
                name: "IdPeeDocument",
                table: "Pee_Document",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<decimal>(
                name: "IdPeriodePeeSuivi",
                table: "Pee_Document",
                type: "decimal(18,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGroupe",
                table: "Evenement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pee_Document",
                table: "Pee_Document",
                column: "IdPeeDocument");

            migrationBuilder.CreateTable(
                name: "Periode_Pee_Suivi",
                columns: table => new
                {
                    IdPeriodePeeSuivi = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPee = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ObjetSuivi = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TexteSuivi = table.Column<string>(type: "varchar(4096)", unicode: false, maxLength: 4096, nullable: true),
                    DateDeSuivi = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periode_Pee_Suivi", x => x.IdPeriodePeeSuivi);
                    table.ForeignKey(
                        name: "Fk_Periode_Pee_Suivi_Pee",
                        column: x => x.IdPee,
                        principalTable: "Pee",
                        principalColumn: "IdPee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pee_Document_IdPee",
                table: "Pee_Document",
                column: "IdPee");

            migrationBuilder.CreateIndex(
                name: "IX_Pee_Document_IdPeriodePeeSuivi",
                table: "Pee_Document",
                column: "IdPeriodePeeSuivi");

            migrationBuilder.CreateIndex(
                name: "IX_Periode_Pee_Suivi_IdPee",
                table: "Periode_Pee_Suivi",
                column: "IdPee");

            migrationBuilder.AddForeignKey(
                name: "FK_Pee_Document_Periode_Pee_Suivi",
                table: "Pee_Document",
                column: "IdPeriodePeeSuivi",
                principalTable: "Periode_Pee_Suivi",
                principalColumn: "IdPeriodePeeSuivi",
                onDelete: ReferentialAction.Restrict);

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
            migrationBuilder.DropForeignKey(
                name: "FK_Pee_Document_Periode_Pee_Suivi",
                table: "Pee_Document");

            migrationBuilder.DropForeignKey(
                name: "CodeTitreCiviliteProfessionnel",
                table: "Professionnel");

            migrationBuilder.DropTable(
                name: "Periode_Pee_Suivi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pee_Document",
                table: "Pee_Document");

            migrationBuilder.DropIndex(
                name: "IX_Pee_Document_IdPee",
                table: "Pee_Document");

            migrationBuilder.DropIndex(
                name: "IX_Pee_Document_IdPeriodePeeSuivi",
                table: "Pee_Document");

            migrationBuilder.DropColumn(
                name: "IdPeeDocument",
                table: "Pee_Document");

            migrationBuilder.DropColumn(
                name: "IdPeriodePeeSuivi",
                table: "Pee_Document");

            migrationBuilder.AlterColumn<decimal>(
                name: "IdPee",
                table: "Pee_Document",
                type: "numeric(18,0)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGroupe",
                table: "Evenement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pee_Document",
                table: "Pee_Document",
                columns: new[] { "IdPee", "NumOrdre" });

            migrationBuilder.CreateTable(
                name: "Periode_Pee_Evenement",
                columns: table => new
                {
                    IdPee = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NumOrdre = table.Column<int>(type: "int", nullable: false),
                    IdEvent = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periode_Pee_Evenement", x => new { x.IdPee, x.NumOrdre, x.IdEvent });
                });

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
