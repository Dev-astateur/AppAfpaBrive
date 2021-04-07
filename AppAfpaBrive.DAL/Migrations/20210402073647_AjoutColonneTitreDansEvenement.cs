using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class AjoutColonneTitreDansEvenement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Professionnel_TitreCivilite",
            //    table: "Professionnel");

            //migrationBuilder.DropColumn(
            //    name: "CodeTitreCivilite",
            //    table: "Professionnel");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGroupe",
                table: "Evenement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Titre",
                table: "Evenement",
                type: "nvarchar(125)",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Professionnel_CodeTitreCiviliteProfessionnel",
            //    table: "Professionnel",
            //    column: "CodeTitreCiviliteProfessionnel");

            //migrationBuilder.AddForeignKey(
            //    name: "CodeTitreCiviliteProfessionnel",
            //    table: "Professionnel",
            //    column: "CodeTitreCiviliteProfessionnel",
            //    principalTable: "TitreCivilite",
            //    principalColumn: "CodeTitreCivilite",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "CodeTitreCiviliteProfessionnel",
            //    table: "Professionnel");

            //migrationBuilder.DropIndex(
            //    name: "IX_Professionnel_CodeTitreCiviliteProfessionnel",
            //    table: "Professionnel");

            migrationBuilder.DropColumn(
                name: "Titre",
                table: "Evenement");

            //migrationBuilder.AddColumn<int>(
            //    name: "CodeTitreCivilite",
            //    table: "Professionnel",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdGroupe",
                table: "Evenement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Professionnel_TitreCivilite",
            //    table: "Professionnel",
            //    column: "CodeTitreCivilite",
            //    principalTable: "TitreCivilite",
            //    principalColumn: "CodeTitreCivilite",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
