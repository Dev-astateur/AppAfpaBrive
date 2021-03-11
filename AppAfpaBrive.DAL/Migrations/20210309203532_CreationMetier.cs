using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppAfpaBrive.DAL.Migrations
{
    public partial class CreationMetier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategorieEvenement",
                columns: table => new
                {
                    IdCatEvent = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    LibelleEvent = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieEvenement", x => x.IdCatEvent);
                });

            migrationBuilder.CreateTable(
                name: "ChampProfessionnnel",
                columns: table => new
                {
                    IdChampProfessionnel = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    LibelleChampProfessionnel = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampProfessionnnel", x => x.IdChampProfessionnel);
                });

            migrationBuilder.CreateTable(
                name: "CodeResultatCertification",
                columns: table => new
                {
                    CodeResultatCertification = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    LibelleResultatCertification = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultatCertification", x => x.CodeResultatCertification);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise_Professionnel",
                columns: table => new
                {
                    IdEntreprise = table.Column<int>(type: "int", nullable: false),
                    IdProfessionnel = table.Column<int>(type: "int", nullable: false),
                    AdresseMailPro = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    TelephonePro = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Fonction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise_Professionnel", x => new { x.IdEntreprise, x.IdProfessionnel });
                });

            migrationBuilder.CreateTable(
                name: "Etablissement",
                columns: table => new
                {
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IdEtablissementRattachement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    NomEtablissement = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MailEtablissement = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    TelEtablissement = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Ligne1Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Ligne2Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Ligne3Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodePostal = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Ville = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissement_1", x => x.IdEtablissement);
                    table.ForeignKey(
                        name: "FK_Etablissement_Etablissement1",
                        column: x => x.IdEtablissementRattachement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilleMetierRome",
                columns: table => new
                {
                    CodeFamilleMetierRome = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    IntituleFamilleMetierRome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilleMetierRome", x => x.CodeFamilleMetierRome);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    IDPays2 = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    IDPays3 = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    IDPaysNum = table.Column<int>(type: "int", nullable: false),
                    LibellePays = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.IDPays2);
                });

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

            migrationBuilder.CreateTable(
                name: "ProduitFormation",
                columns: table => new
                {
                    CodeProduitFormation = table.Column<int>(type: "int", nullable: false),
                    NiveauFormation = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: true),
                    LibelleProduitFormation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    LibelleCourtFormation = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    FormationContinue = table.Column<bool>(type: "bit", nullable: false),
                    FormationDiplomante = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitDeFormation", x => x.CodeProduitFormation);
                });

            migrationBuilder.CreateTable(
                name: "Professionnel",
                columns: table => new
                {
                    IdProfessionnel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProfessionnel = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PrenomProfessionnel = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CodeTitreCiviliteProfessionnel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionnel", x => x.IdProfessionnel);
                });

            migrationBuilder.CreateTable(
                name: "TitreCivilite",
                columns: table => new
                {
                    CodeTitreCivilite = table.Column<int>(type: "int", nullable: false),
                    TitreCiviliteAbrege = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: false),
                    TitreCiviliteComplet = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitreCivilite", x => x.CodeTitreCivilite);
                });

            migrationBuilder.CreateTable(
                name: "TypeContrat",
                columns: table => new
                {
                    IdTypeContrat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    designationTypeContrat = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeContrat", x => x.IdTypeContrat);
                });

            migrationBuilder.CreateTable(
                name: "UniteOrganisationnelle",
                columns: table => new
                {
                    UO = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    LibelleUO = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniteOrganisationnelle", x => x.UO);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    IdEvent = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategorieEvent = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    DateEvent = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    DétailsEvent = table.Column<string>(type: "varchar(5000)", unicode: false, maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.IdEvent);
                    table.ForeignKey(
                        name: "FK_Evenement_CategorieEvenement",
                        column: x => x.IdCategorieEvent,
                        principalTable: "CategorieEvenement",
                        principalColumn: "IdCatEvent",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evenement_Etablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DomaineMetierRome",
                columns: table => new
                {
                    CodeDomaineRome = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    IntituleDomaineRome = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CodeFamilleRome = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomaineMetierRome", x => x.CodeDomaineRome);
                    table.ForeignKey(
                        name: "FK_DomaineMetierRome_FamilleMetierRome",
                        column: x => x.CodeFamilleRome,
                        principalTable: "FamilleMetierRome",
                        principalColumn: "CodeFamilleMetierRome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    IdEntreprise = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaisonSociale = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    NumeroSIRET = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    MailEntreprise = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    TelEntreprise = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Ligne1Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Ligne2Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Ligne3Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodePostal = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Ville = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IDPays2 = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.IdEntreprise);
                    table.ForeignKey(
                        name: "FK_Entreprise_Pays",
                        column: x => x.IDPays2,
                        principalTable: "Pays",
                        principalColumn: "IDPays2",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaire",
                columns: table => new
                {
                    MatriculeBeneficiaire = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    CodeTitreCivilite = table.Column<int>(type: "int", nullable: false),
                    NomBeneficiaire = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PrenomBeneficiaire = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DateNaissanceBeneficiaire = table.Column<DateTime>(type: "date", nullable: true),
                    MailBeneficiaire = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    TelBeneficiaire = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Ligne1Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Ligne2Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Ligne3Adresse = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CodePostal = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Ville = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IdPays2 = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    PathPhoto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MailingAutorise = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaire", x => x.MatriculeBeneficiaire);
                    table.ForeignKey(
                        name: "FK_Beneficiaire_TitreCivilite",
                        column: x => x.CodeTitreCivilite,
                        principalTable: "TitreCivilite",
                        principalColumn: "CodeTitreCivilite",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborateurAfpa",
                columns: table => new
                {
                    MatriculeCollaborateurAfpa = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    CodeTitreCivilite = table.Column<int>(type: "int", nullable: false),
                    NomCollaborateur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrenomCollaborateur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MailCollaborateurAfpa = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    TelCollaborateurAfpa = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UO = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborateurAfpa_1", x => x.MatriculeCollaborateurAfpa);
                    table.ForeignKey(
                        name: "FK_CollaborateurAfpa_Etablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborateurAfpa_TitreCivilite",
                        column: x => x.CodeTitreCivilite,
                        principalTable: "TitreCivilite",
                        principalColumn: "CodeTitreCivilite",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborateurAfpa_UniteOrganisationnelle",
                        column: x => x.UO,
                        principalTable: "UniteOrganisationnelle",
                        principalColumn: "UO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UniteOrganisationnelle_ChampProfessionnel",
                columns: table => new
                {
                    UO = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    IdChampProfessionnel = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uo_ChampProfessionnel", x => new { x.UO, x.IdChampProfessionnel });
                    table.ForeignKey(
                        name: "FK_Uo_ChampProfessionnel_ChampProfessionnnel",
                        column: x => x.IdChampProfessionnel,
                        principalTable: "ChampProfessionnnel",
                        principalColumn: "IdChampProfessionnel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Uo_ChampProfessionnel_UniteOrganisationnelle",
                        column: x => x.UO,
                        principalTable: "UniteOrganisationnelle",
                        principalColumn: "UO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evenement_Document",
                columns: table => new
                {
                    IdEvent = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NumOrdre = table.Column<int>(type: "int", nullable: false),
                    PathDocument = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement_Document", x => new { x.IdEvent, x.NumOrdre });
                    table.ForeignKey(
                        name: "FK_Evenement_Document_Evenement",
                        column: x => x.IdEvent,
                        principalTable: "Evenement",
                        principalColumn: "IdEvent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rome",
                columns: table => new
                {
                    CodeRome = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    IntituleCodeRome = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CodeDomaineRome = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeRome", x => x.CodeRome);
                    table.ForeignKey(
                        name: "FK_Rome_DomaineMetierRome",
                        column: x => x.CodeDomaineRome,
                        principalTable: "DomaineMetierRome",
                        principalColumn: "CodeDomaineRome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrat",
                columns: table => new
                {
                    IdContrat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEntreprise = table.Column<int>(type: "int", nullable: false),
                    MatriculeBeneficiaire = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    CodeAppellation = table.Column<int>(type: "int", nullable: true),
                    DateEntreeFonction = table.Column<DateTime>(type: "date", nullable: false),
                    DateSortieFonction = table.Column<DateTime>(type: "date", nullable: true),
                    TypeContrat = table.Column<int>(type: "int", nullable: false),
                    DureeContratMois = table.Column<int>(type: "int", nullable: false),
                    EnLienMetierFormation = table.Column<bool>(type: "bit", nullable: false),
                    LibelleFonction = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrat", x => x.IdContrat);
                    table.ForeignKey(
                        name: "FK_Contrat_Beneficiaire",
                        column: x => x.MatriculeBeneficiaire,
                        principalTable: "Beneficiaire",
                        principalColumn: "MatriculeBeneficiaire",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrat_Entreprise",
                        column: x => x.IdEntreprise,
                        principalTable: "Entreprise",
                        principalColumn: "IdEntreprise",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrat_TypeContrat",
                        column: x => x.TypeContrat,
                        principalTable: "TypeContrat",
                        principalColumn: "IdTypeContrat",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OffreFormation",
                columns: table => new
                {
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    MatriculeCollaborateurAfpa = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: true),
                    CodeProduitFormation = table.Column<int>(type: "int", nullable: false),
                    LibelleOffreFormation = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    LibelleReduitOffreFormation = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DateDebutOffreFormation = table.Column<DateTime>(type: "date", nullable: false),
                    DateFinOffreFormation = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffreFormation", x => new { x.IdOffreFormation, x.IdEtablissement });
                    table.ForeignKey(
                        name: "FK_OffreFormation_CollaborateurAfpa",
                        column: x => x.MatriculeCollaborateurAfpa,
                        principalTable: "CollaborateurAfpa",
                        principalColumn: "MatriculeCollaborateurAfpa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffreFormation_Etablissement",
                        column: x => x.IdEtablissement,
                        principalTable: "Etablissement",
                        principalColumn: "IdEtablissement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OffreFormation_ProduitDeFormation",
                        column: x => x.CodeProduitFormation,
                        principalTable: "ProduitFormation",
                        principalColumn: "CodeProduitFormation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppelationRome",
                columns: table => new
                {
                    CodeAppelationRome = table.Column<int>(type: "int", nullable: false),
                    LibelleAppelationRome = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CodeRome = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppelationRome", x => x.CodeAppelationRome);
                    table.ForeignKey(
                        name: "FK_AppelationRome_CodeRome",
                        column: x => x.CodeRome,
                        principalTable: "Rome",
                        principalColumn: "CodeRome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProduitFormation_AppellationRome",
                columns: table => new
                {
                    CodeProduitFormation = table.Column<int>(type: "int", nullable: false),
                    CodeRome = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitFormation_AppellationRome", x => new { x.CodeProduitFormation, x.CodeRome });
                    table.ForeignKey(
                        name: "FK_ProduitFormation_AppellationRome_ProduitFormation",
                        column: x => x.CodeProduitFormation,
                        principalTable: "ProduitFormation",
                        principalColumn: "CodeProduitFormation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduitFormation_AppellationRome_Rome",
                        column: x => x.CodeRome,
                        principalTable: "Rome",
                        principalColumn: "CodeRome",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaire_OffreFormation",
                columns: table => new
                {
                    MatriculeBeneficiaire = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    IDEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    DateEntreeBeneficiaire = table.Column<DateTime>(type: "date", nullable: false),
                    DateSortieBeneficiaire = table.Column<DateTime>(type: "date", nullable: true),
                    Certifie = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true, defaultValueSql: "('ANT')"),
                    Consultable = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    Delegue = table.Column<bool>(type: "bit", nullable: true),
                    Suppleant = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaire_OffreFormation", x => new { x.MatriculeBeneficiaire, x.IdOffreFormation, x.IDEtablissement });
                    table.ForeignKey(
                        name: "FK_Beneficiaire_OffreFormation_Beneficiaire",
                        column: x => x.MatriculeBeneficiaire,
                        principalTable: "Beneficiaire",
                        principalColumn: "MatriculeBeneficiaire",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaire_OffreFormation_OffreFormation",
                        columns: x => new { x.IdOffreFormation, x.IDEtablissement },
                        principalTable: "OffreFormation",
                        principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaire_OffreFormation_ResultatCertification",
                        column: x => x.Certifie,
                        principalTable: "CodeResultatCertification",
                        principalColumn: "CodeResultatCertification",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampagneMail",
                columns: table => new
                {
                    IdCampagneMail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampagneMail", x => x.IdCampagneMail);
                    table.ForeignKey(
                        name: "FK_CampagneMail_OffreFormation",
                        columns: x => new { x.IdOffreFormation, x.IdEtablissement },
                        principalTable: "OffreFormation",
                        principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pee",
                columns: table => new
                {
                    IdPee = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatriculeBeneficiaire = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    IdTuteur = table.Column<int>(type: "int", nullable: false),
                    IdResponsableJuridique = table.Column<int>(type: "int", nullable: false),
                    IdEntreprise = table.Column<int>(type: "int", nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pee", x => x.IdPee);
                    table.ForeignKey(
                        name: "FK_Pee_Beneficiaire",
                        column: x => x.MatriculeBeneficiaire,
                        principalTable: "Beneficiaire",
                        principalColumn: "MatriculeBeneficiaire",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pee_OffreFormation",
                        columns: x => new { x.IdOffreFormation, x.IdEtablissement },
                        principalTable: "OffreFormation",
                        principalColumns: new[] { "IdOffreFormation", "IdEtablissement" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pee_ResponsableJuridique",
                        column: x => x.IdResponsableJuridique,
                        principalTable: "Professionnel",
                        principalColumn: "IdProfessionnel",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pee_Tuteur",
                        column: x => x.IdTuteur,
                        principalTable: "Professionnel",
                        principalColumn: "IdProfessionnel",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationCampagneMail",
                columns: table => new
                {
                    IdPlanificationCampagneMail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCampagneMail = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    Echeance = table.Column<DateTime>(type: "date", nullable: false),
                    DateRealisation = table.Column<DateTime>(type: "date", nullable: true),
                    NombreDestinataires = table.Column<int>(type: "int", nullable: false),
                    NombreEnvois = table.Column<int>(type: "int", nullable: false),
                    NombreReponses = table.Column<int>(type: "int", nullable: false),
                    EtatTraitement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationCampagne", x => x.IdPlanificationCampagneMail);
                    table.ForeignKey(
                        name: "FK_PlanificationCampagneMail_CampagneMail",
                        column: x => x.IdCampagneMail,
                        principalTable: "CampagneMail",
                        principalColumn: "IdCampagneMail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Periode_Pee",
                columns: table => new
                {
                    IdPee = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NumOrdre = table.Column<int>(type: "int", nullable: false),
                    DateDebutPeriodePee = table.Column<DateTime>(type: "date", nullable: false),
                    DateFinPeriodePee = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periode_Pee_1", x => new { x.IdPee, x.NumOrdre });
                    table.ForeignKey(
                        name: "FK_Periode_Pee_Pee",
                        column: x => x.IdPee,
                        principalTable: "Pee",
                        principalColumn: "IdPee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DestinataireEnquete",
                columns: table => new
                {
                    IdSoumissionnaire = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlanificationCampagneMail = table.Column<int>(type: "int", nullable: false),
                    MatriculeBeneficiaire = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    IdOffreFormation = table.Column<int>(type: "int", nullable: false),
                    IdEtablissement = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    DateEcheanceEnquete = table.Column<DateTime>(type: "date", nullable: false),
                    DateRealisationEnquete = table.Column<DateTime>(type: "datetime", nullable: true),
                    EtatTraitementQuestionnaire = table.Column<int>(type: "int", nullable: false),
                    Agrege = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "('0')"),
                    EtapeQuestionnaire = table.Column<int>(type: "int", nullable: true),
                    DateRelance1 = table.Column<DateTime>(type: "date", nullable: true),
                    DateRelance2 = table.Column<DateTime>(type: "date", nullable: true),
                    IdContrat = table.Column<int>(type: "int", nullable: true),
                    EnEmploi = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinataireEnquete", x => x.IdSoumissionnaire);
                    table.ForeignKey(
                        name: "FK_DestinataireEnquete_Beneficiaire",
                        column: x => x.MatriculeBeneficiaire,
                        principalTable: "Beneficiaire",
                        principalColumn: "MatriculeBeneficiaire",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DestinataireEnquete_Contrat",
                        column: x => x.IdContrat,
                        principalTable: "Contrat",
                        principalColumn: "IdContrat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DestinataireEnquete_PlanificationCampagneMail",
                        column: x => x.IdPlanificationCampagneMail,
                        principalTable: "PlanificationCampagneMail",
                        principalColumn: "IdPlanificationCampagneMail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppelationRome_CodeRome",
                table: "AppelationRome",
                column: "CodeRome");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaire_CodeTitreCivilite",
                table: "Beneficiaire",
                column: "CodeTitreCivilite");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaire_OffreFormation_Certifie",
                table: "Beneficiaire_OffreFormation",
                column: "Certifie");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaire_OffreFormation_IdOffreFormation_IDEtablissement",
                table: "Beneficiaire_OffreFormation",
                columns: new[] { "IdOffreFormation", "IDEtablissement" });

            migrationBuilder.CreateIndex(
                name: "IX_CampagneMail_IdOffreFormation_IdEtablissement",
                table: "CampagneMail",
                columns: new[] { "IdOffreFormation", "IdEtablissement" });

            migrationBuilder.CreateIndex(
                name: "IX_CollaborateurAfpa_CodeTitreCivilite",
                table: "CollaborateurAfpa",
                column: "CodeTitreCivilite");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborateurAfpa_IdEtablissement",
                table: "CollaborateurAfpa",
                column: "IdEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborateurAfpa_UO",
                table: "CollaborateurAfpa",
                column: "UO");

            migrationBuilder.CreateIndex(
                name: "IX_Contrat_IdEntreprise",
                table: "Contrat",
                column: "IdEntreprise");

            migrationBuilder.CreateIndex(
                name: "IX_Contrat_MatriculeBeneficiaire",
                table: "Contrat",
                column: "MatriculeBeneficiaire");

            migrationBuilder.CreateIndex(
                name: "IX_Contrat_TypeContrat",
                table: "Contrat",
                column: "TypeContrat");

            migrationBuilder.CreateIndex(
                name: "IX_DestinataireEnquete_IdContrat",
                table: "DestinataireEnquete",
                column: "IdContrat");

            migrationBuilder.CreateIndex(
                name: "IX_DestinataireEnquete_IdPlanificationCampagneMail",
                table: "DestinataireEnquete",
                column: "IdPlanificationCampagneMail");

            migrationBuilder.CreateIndex(
                name: "IX_DestinataireEnquete_MatriculeBeneficiaire",
                table: "DestinataireEnquete",
                column: "MatriculeBeneficiaire");

            migrationBuilder.CreateIndex(
                name: "IX_DomaineMetierRome_CodeFamilleRome",
                table: "DomaineMetierRome",
                column: "CodeFamilleRome");

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_IDPays2",
                table: "Entreprise",
                column: "IDPays2");

            migrationBuilder.CreateIndex(
                name: "IX_Etablissement_IdEtablissementRattachement",
                table: "Etablissement",
                column: "IdEtablissementRattachement");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_IdCategorieEvent",
                table: "Evenement",
                column: "IdCategorieEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_IdEtablissement",
                table: "Evenement",
                column: "IdEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_OffreFormation_CodeProduitFormation",
                table: "OffreFormation",
                column: "CodeProduitFormation");

            migrationBuilder.CreateIndex(
                name: "IX_OffreFormation_IdEtablissement",
                table: "OffreFormation",
                column: "IdEtablissement");

            migrationBuilder.CreateIndex(
                name: "IX_OffreFormation_MatriculeCollaborateurAfpa",
                table: "OffreFormation",
                column: "MatriculeCollaborateurAfpa");

            migrationBuilder.CreateIndex(
                name: "IX_Pee_IdOffreFormation_IdEtablissement",
                table: "Pee",
                columns: new[] { "IdOffreFormation", "IdEtablissement" });

            migrationBuilder.CreateIndex(
                name: "IX_Pee_IdResponsableJuridique",
                table: "Pee",
                column: "IdResponsableJuridique");

            migrationBuilder.CreateIndex(
                name: "IX_Pee_IdTuteur",
                table: "Pee",
                column: "IdTuteur");

            migrationBuilder.CreateIndex(
                name: "IX_Pee_MatriculeBeneficiaire",
                table: "Pee",
                column: "MatriculeBeneficiaire");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationCampagneMail_IdCampagneMail",
                table: "PlanificationCampagneMail",
                column: "IdCampagneMail");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitFormation_AppellationRome_CodeRome",
                table: "ProduitFormation_AppellationRome",
                column: "CodeRome");

            migrationBuilder.CreateIndex(
                name: "IX_Rome_CodeDomaineRome",
                table: "Rome",
                column: "CodeDomaineRome");

            migrationBuilder.CreateIndex(
                name: "IX_UniteOrganisationnelle_ChampProfessionnel_IdChampProfessionnel",
                table: "UniteOrganisationnelle_ChampProfessionnel",
                column: "IdChampProfessionnel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppelationRome");

            migrationBuilder.DropTable(
                name: "Beneficiaire_OffreFormation");

            migrationBuilder.DropTable(
                name: "DestinataireEnquete");

            migrationBuilder.DropTable(
                name: "Entreprise_Professionnel");

            migrationBuilder.DropTable(
                name: "Evenement_Document");

            migrationBuilder.DropTable(
                name: "Periode_Pee");

            migrationBuilder.DropTable(
                name: "Periode_Pee_Evenement");

            migrationBuilder.DropTable(
                name: "ProduitFormation_AppellationRome");

            migrationBuilder.DropTable(
                name: "UniteOrganisationnelle_ChampProfessionnel");

            migrationBuilder.DropTable(
                name: "CodeResultatCertification");

            migrationBuilder.DropTable(
                name: "Contrat");

            migrationBuilder.DropTable(
                name: "PlanificationCampagneMail");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Pee");

            migrationBuilder.DropTable(
                name: "Rome");

            migrationBuilder.DropTable(
                name: "ChampProfessionnnel");

            migrationBuilder.DropTable(
                name: "Entreprise");

            migrationBuilder.DropTable(
                name: "TypeContrat");

            migrationBuilder.DropTable(
                name: "CampagneMail");

            migrationBuilder.DropTable(
                name: "CategorieEvenement");

            migrationBuilder.DropTable(
                name: "Beneficiaire");

            migrationBuilder.DropTable(
                name: "Professionnel");

            migrationBuilder.DropTable(
                name: "DomaineMetierRome");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "OffreFormation");

            migrationBuilder.DropTable(
                name: "FamilleMetierRome");

            migrationBuilder.DropTable(
                name: "CollaborateurAfpa");

            migrationBuilder.DropTable(
                name: "ProduitFormation");

            migrationBuilder.DropTable(
                name: "Etablissement");

            migrationBuilder.DropTable(
                name: "TitreCivilite");

            migrationBuilder.DropTable(
                name: "UniteOrganisationnelle");
        }
    }
}
