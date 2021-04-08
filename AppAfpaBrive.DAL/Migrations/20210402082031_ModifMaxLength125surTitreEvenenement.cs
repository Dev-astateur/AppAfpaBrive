using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class ModifMaxLength125surTitreEvenenement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Evenement",
                type: "nvarchar(125)",
                maxLength: 125,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Evenement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(125)",
                oldMaxLength: 125,
                oldNullable: true);
        }
    }
}
