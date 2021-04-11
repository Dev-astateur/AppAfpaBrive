using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class Correction_PeeDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathDocument",
                table: "Pee_Document"
                );
            migrationBuilder.AddColumn<string>(
                name: "PathDocument",
                table: "Pee_Document",
                type: "varchar(2048)",
                unicode: false,
                maxLength: 2048,
                nullable: true
                //defaultValue: ""
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathDocument",
                table: "Pee_Document",
                type: "varchar(2048)",
                unicode: false,
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2048)",
                oldUnicode: false,
                oldMaxLength: 2048);
        }
    }
}
