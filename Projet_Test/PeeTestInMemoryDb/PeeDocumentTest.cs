using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Projet_Test.InMemoryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test.PeeTestInMemoryDb
{
    public class PeeDocumentTest
    {

        AFPANADbContext _context;


        Beneficiaire CASTEL = new Beneficiaire { MatriculeBeneficiaire = "16174318", CodeTitreCivilite = 0, NomBeneficiaire = "CASTEL", PrenomBeneficiaire = "MAXIME" };
        Beneficiaire ETCHART = new Beneficiaire { MatriculeBeneficiaire = "20035347", CodeTitreCivilite = 0, NomBeneficiaire = "ETCHART", PrenomBeneficiaire = "PIERRE" };
        Beneficiaire ABRAHAM = new Beneficiaire { MatriculeBeneficiaire = "20022801", CodeTitreCivilite = 0, NomBeneficiaire = "ABRAHAM", PrenomBeneficiaire = "DENZEL" };
        Entreprise entreprise1 = new Entreprise { IdEntreprise = 2, RaisonSociale = "CAI", NumeroSiret = "42159769100029", Ligne1Adresse = "5, boulevard Mirabeau", CodePostal = "19100", Ville = "Brive la Gaillarde" };
        Entreprise entreprise2 = new Entreprise { IdEntreprise = 4, RaisonSociale = "ANDROS SNC", NumeroSiret = "42868244700019", Ligne1Adresse = "ZI", CodePostal = "46130", Ville = "Biars-sur-Cère" };
        Entreprise entreprise3 = new Entreprise { IdEntreprise = 5, RaisonSociale = "Université de Limoges DSI", NumeroSiret = "19870669900321", Ligne1Adresse = "123 Avenue Albert Thomas", CodePostal = "87060", Ville = "Limoges Cedex" };
        PeriodePee periodePee1 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021, 04 , 12), DateFinPeriodePee = new DateTime(2021,06, 28), IdPee = 4, NumOrdre = 1 };
        PeriodePee periodePee2 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021 , 04 , 12), DateFinPeriodePee = new DateTime(2021, 06, 28), IdPee = 5, NumOrdre = 2 };
        PeriodePee periodePee3 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021 , 04 , 12), DateFinPeriodePee = new DateTime(2021 , 06, 28), IdPee = 6, NumOrdre = 3 };
        Professionnel Lucas = new Professionnel { IdProfessionnel = 12, NomProfessionnel = "Lucas", PrenomProfessionnel = "Grégoire", CodeTitreCiviliteProfessionnel = 0 };
        Professionnel Raymond = new Professionnel { IdProfessionnel = 2, NomProfessionnel = "Raymond", PrenomProfessionnel = "Sophie", CodeTitreCiviliteProfessionnel = 1 };
        Professionnel Domme = new Professionnel { IdProfessionnel = 1, NomProfessionnel = "Domme", PrenomProfessionnel = "Sébastien", CodeTitreCiviliteProfessionnel = 0 };
        Professionnel Blackmann = new Professionnel { IdProfessionnel = 11, NomProfessionnel = "Blackmann", PrenomProfessionnel = "Rudy", CodeTitreCiviliteProfessionnel = 0 };
        Professionnel Bebronne = new Professionnel { IdProfessionnel = 3, NomProfessionnel = "Bebronne", PrenomProfessionnel = "Julien", CodeTitreCiviliteProfessionnel = 0 };
        OffreFormation Concepteur = new OffreFormation { IdOffreFormation = 20102, IdEtablissement = "19011", MatriculeCollaborateurAfpa = "1603870", CodeProduitFormation = 12226, LibelleOffreFormation = "Concepteur développeur d'applications JO2018.1", LibelleReduitOffreFormation = "", DateDebutOffreFormation = new DateTime(2020 - 09 - 01), DateFinOffreFormation = new DateTime(2021 - 06 - 30) };
        OffreFormation Technicien = new OffreFormation { IdOffreFormation = 20101, IdEtablissement = "19011", MatriculeCollaborateurAfpa = "96GB011", CodeProduitFormation = 9952, LibelleOffreFormation = "Technicien supérieur systèmes et réseaux JO2018.1", LibelleReduitOffreFormation = "", DateDebutOffreFormation = new DateTime(2020 - 09 - 07), DateFinOffreFormation = new DateTime(2021 - 06 - 11) };
        TitreCivilite Monsieur = new TitreCivilite { CodeTitreCivilite = 0, TitreCiviliteAbrege = "M", TitreCiviliteComplet = "Monsieur" };
        TitreCivilite Madame = new TitreCivilite { CodeTitreCivilite = 1, TitreCiviliteAbrege = "MME", TitreCiviliteComplet = "Madame" };
        EntrepriseProfessionnel EntrepriseProfessionnel1 = new EntrepriseProfessionnel { IdEntreprise = 2, IdProfessionnel = 1, Actif = true, Fonction = "Chef de projets" };
        EntrepriseProfessionnel EntrepriseProfessionnel2 = new EntrepriseProfessionnel { IdEntreprise = 4, IdProfessionnel = 2, Actif = true,Fonction = "" };
        EntrepriseProfessionnel EntrepriseProfessionnel3 = new EntrepriseProfessionnel { IdEntreprise = 5, IdProfessionnel = 4, Actif = true, Fonction="" };
        EntrepriseProfessionnel EntrepriseProfessionnel4 = new EntrepriseProfessionnel { IdEntreprise = 5, IdProfessionnel = 5, AdresseMailPro = "nicolas.viers@unilim.fr", Actif = true, Fonction=null };
        CollaborateurAfpa Bueno = new CollaborateurAfpa { MatriculeCollaborateurAfpa = "1603870", IdEtablissement = "19011", CodeTitreCivilite = 0, NomCollaborateur = "Bueno", PrenomCollaborateur = "Ange", MailCollaborateurAfpa = "ange.bueno@afpa.fr", TelCollaborateurAfpa = "06 03 37 02 94", Uo = "164", UserId = "" };
        CollaborateurAfpa bost = new CollaborateurAfpa { MatriculeCollaborateurAfpa = "96GB011", IdEtablissement = "19011", CodeTitreCivilite = 0, NomCollaborateur = "bost", PrenomCollaborateur = "Vincent", MailCollaborateurAfpa = "vincent.bost@afpa.fr", Uo = "164" };
     //   HashSet<PeriodePee> per1 = new HashSet<PeriodePee>();


        public AFPANADbContext GetPee()
        {
            _context =  DbContextMocker.GetAFPANADbContext("AFPANA");
          //  per1.Add(periodePee1);
          _context.Pees.AddRange(new Pee
            {
                IdPee = 4,
                MatriculeBeneficiaire = "16174318",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20102,
                IdEtablissement = "19011",
                Etat = 0,
                MatriculeBeneficiaireNavigation = CASTEL,
                IdEntrepriseNavigation = entreprise1,
                IdResponsableJuridiqueNavigation = Lucas,
                IdTuteurNavigation = Blackmann,
                Id = Concepteur
                
          },
            new Pee
            {
                IdPee = 5,
                MatriculeBeneficiaire = "20035347",
                IdTuteur = 3,
                IdResponsableJuridique = 2,
                IdEntreprise = 4,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                Etat = 0,
                MatriculeBeneficiaireNavigation = ETCHART,
                IdEntrepriseNavigation = entreprise2,
                IdResponsableJuridiqueNavigation = Raymond,
                IdTuteurNavigation = Bebronne,
                Id = Technicien
            },
            new Pee
            {
                IdPee = 6,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 5,
                IdResponsableJuridique = 4,
                IdEntreprise = 5,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                Etat = 0,
                MatriculeBeneficiaireNavigation = ABRAHAM,
                IdEntrepriseNavigation = entreprise3,
                IdResponsableJuridiqueNavigation = Domme,
                IdTuteurNavigation = Domme,
                Id = Technicien
            });
            _context.PeriodePees.AddRange(periodePee1, periodePee2, periodePee3
               );
            _context.TitreCivilites.AddRange(Madame, Monsieur);
            _context.EntrepriseProfessionnels.AddRange(EntrepriseProfessionnel1, EntrepriseProfessionnel2, EntrepriseProfessionnel3, EntrepriseProfessionnel4);
            _context.CollaborateurAfpas.AddRange(Bueno, bost);
            _context.SaveChanges();
            return _context;
        }

    }
}
