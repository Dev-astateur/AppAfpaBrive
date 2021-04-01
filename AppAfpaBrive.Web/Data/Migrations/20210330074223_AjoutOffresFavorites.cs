using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.Web.Data.Migrations
{
    public partial class AjoutOffresFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "OffresFavorites",
               table: "AspNetUsers",
               type: "nvarchar(2058)",
               nullable: true);
         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "OffresFavorites",
               table: "AspNetUsers");
        }
    }
}
