using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class AnnuaireSocial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Repondu",
                table: "DestinataireEnquete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleCategorie = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategorie);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    IdContact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Prenom = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Mail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IdTitreCivilite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.IdContact);
                    table.ForeignKey(
                        name: "FK_Contacts_TitreCivilite_IdTitreCivilite",
                        column: x => x.IdTitreCivilite,
                        principalTable: "TitreCivilite",
                        principalColumn: "CodeTitreCivilite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Structures",
                columns: table => new
                {
                    IdStructure = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomStructure = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LigneAdresse1 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LigneAdresse2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LigneAdresse3 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodePostal = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Ville = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Mail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structures", x => x.IdStructure);
                });

            migrationBuilder.CreateTable(
                name: "ContactStructures",
                columns: table => new
                {
                    IdContact = table.Column<int>(type: "int", nullable: false),
                    IdStructure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStructures", x => new { x.IdContact, x.IdStructure });
                    table.ForeignKey(
                        name: "FK_ContactStructures_Contacts_IdContact",
                        column: x => x.IdContact,
                        principalTable: "Contacts",
                        principalColumn: "IdContact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactStructures_Structures_IdStructure",
                        column: x => x.IdStructure,
                        principalTable: "Structures",
                        principalColumn: "IdStructure",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LigneAnnuaires",
                columns: table => new
                {
                    IdLigneAnnuaire = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicConcerne = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ServiceAbrege = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conditions = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    IdStructure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneAnnuaires", x => x.IdLigneAnnuaire);
                    table.ForeignKey(
                        name: "FK_LigneAnnuaires_Structures_IdStructure",
                        column: x => x.IdStructure,
                        principalTable: "Structures",
                        principalColumn: "IdStructure",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorieLigneAnnuaires",
                columns: table => new
                {
                    IdLigneAnnuaire = table.Column<int>(type: "int", nullable: false),
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieLigneAnnuaires", x => new { x.IdCategorie, x.IdLigneAnnuaire });
                    table.ForeignKey(
                        name: "FK_CategorieLigneAnnuaires_Categories_IdCategorie",
                        column: x => x.IdCategorie,
                        principalTable: "Categories",
                        principalColumn: "IdCategorie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieLigneAnnuaires_LigneAnnuaires_IdLigneAnnuaire",
                        column: x => x.IdLigneAnnuaire,
                        principalTable: "LigneAnnuaires",
                        principalColumn: "IdLigneAnnuaire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactLigneAnnuaires",
                columns: table => new
                {
                    IdContact = table.Column<int>(type: "int", nullable: false),
                    IdLigneAnnuaire = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLigneAnnuaires", x => new { x.IdContact, x.IdLigneAnnuaire });
                    table.ForeignKey(
                        name: "FK_ContactLigneAnnuaires_Contacts_IdContact",
                        column: x => x.IdContact,
                        principalTable: "Contacts",
                        principalColumn: "IdContact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactLigneAnnuaires_LigneAnnuaires_IdLigneAnnuaire",
                        column: x => x.IdLigneAnnuaire,
                        principalTable: "LigneAnnuaires",
                        principalColumn: "IdLigneAnnuaire",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professionnel_CodeTitreCiviliteProfessionnel",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieLigneAnnuaires_IdLigneAnnuaire",
                table: "CategorieLigneAnnuaires",
                column: "IdLigneAnnuaire");

            migrationBuilder.CreateIndex(
                name: "IX_ContactLigneAnnuaires_IdLigneAnnuaire",
                table: "ContactLigneAnnuaires",
                column: "IdLigneAnnuaire");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_IdTitreCivilite",
                table: "Contacts",
                column: "IdTitreCivilite");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStructures_IdStructure",
                table: "ContactStructures",
                column: "IdStructure");

            migrationBuilder.CreateIndex(
                name: "IX_LigneAnnuaires_IdStructure",
                table: "LigneAnnuaires",
                column: "IdStructure");

            migrationBuilder.AddForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel",
                column: "CodeTitreCiviliteProfessionnel",
                principalTable: "TitreCivilite",
                principalColumn: "CodeTitreCivilite",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professionnel_TitreCivilite",
                table: "Professionnel");

            migrationBuilder.DropTable(
                name: "CategorieLigneAnnuaires");

            migrationBuilder.DropTable(
                name: "ContactLigneAnnuaires");

            migrationBuilder.DropTable(
                name: "ContactStructures");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "LigneAnnuaires");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Structures");

            migrationBuilder.DropIndex(
                name: "IX_Professionnel_CodeTitreCiviliteProfessionnel",
                table: "Professionnel");

            migrationBuilder.DropColumn(
                name: "Repondu",
                table: "DestinataireEnquete");
        }
    }
}
