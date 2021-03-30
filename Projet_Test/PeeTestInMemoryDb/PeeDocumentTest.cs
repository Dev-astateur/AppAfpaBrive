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
        DbContextMocker _dbContext;
        AFPANADbContext _context;
        public PeeDocumentTest(DbContextMocker dbContextMocker)
        {
            _context = dbContextMocker.GetAFPANADbContext("AFPANA");
        }
        
        Beneficiaire CASTEL = new Beneficiaire { MatriculeBeneficiaire = "16174318", CodeTitreCivilite = 0, NomBeneficiaire = "CASTEL", PrenomBeneficiaire = "MAXIME" };
        Beneficiaire MORGAN = new Beneficiaire { MatriculeBeneficiaire = "18128730", CodeTitreCivilite = 1, NomBeneficiaire = "ELIAS", PrenomBeneficiaire = "MORGAN" };
        Beneficiaire ABRAHAM = new Beneficiaire { MatriculeBeneficiaire = "20022801", CodeTitreCivilite = 0, NomBeneficiaire = "ABRAHAM", PrenomBeneficiaire = "DENZEL" };
        Entreprise entreprise1 = new Entreprise { IdEntreprise = 2, RaisonSociale = "CAI", NumeroSiret = "42159769100029", Ligne1Adresse = "5, boulevard Mirabeau", CodePostal = "19100", Ville = "Brive la Gaillarde" };
        Entreprise entreprise2 = new Entreprise { IdEntreprise = 4, RaisonSociale = "ANDROS SNC", NumeroSiret = "42868244700019", Ligne1Adresse = "ZI", CodePostal = "46130", Ville = "Biars-sur-Cère" };
        Entreprise entreprise3 = new Entreprise { IdEntreprise = 5, RaisonSociale = "Université de Limoges DSI", NumeroSiret = "19870669900321", Ligne1Adresse = "123 Avenue Albert Thomas", CodePostal = "87060", Ville = "Limoges Cedex" };
        PeriodePee periodePee1 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021 - 04 - 12), DateFinPeriodePee = new DateTime(2021 - 06 - 28), IdPee = 4, NumOrdre = 1 };
        PeriodePee periodePee2 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021 - 04 - 12), DateFinPeriodePee = new DateTime(2021 - 06 - 28), IdPee = 5, NumOrdre = 2 };
        PeriodePee periodePee3 = new PeriodePee { DateDebutPeriodePee = new DateTime(2021 - 04 - 12), DateFinPeriodePee = new DateTime(2021 - 06 - 28), IdPee = 5, NumOrdre = 3 };

        public AFPANADbContext GetPee()
        {
           
          _context.Pees.AddRange(new Pee
            {
                IdPee = 4,
                MatriculeBeneficiaire = "16174318",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                Etat = 0,
                MatriculeBeneficiaireNavigation = CASTEL,
                IdEntrepriseNavigation = entreprise1,
            },
            new Pee
            {
                IdPee = 5,
                MatriculeBeneficiaire = "18128730",
                IdTuteur = 3,
                IdResponsableJuridique = 2,
                IdEntreprise = 4,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                Etat = 0,
                MatriculeBeneficiaireNavigation = MORGAN,
                IdEntrepriseNavigation = entreprise2
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
                IdEntrepriseNavigation = entreprise3
            });
            _context.PeriodePees.AddRange(
                new PeriodePee 
                {
                    DateDebutPeriodePee = new DateTime(2021 - 04 - 12), 
                    DateFinPeriodePee = new DateTime(2021 - 06 - 28),
                    IdPee = 4, NumOrdre = 1 },
                new PeriodePee 
                { 
                    DateDebutPeriodePee = new DateTime(2021 - 04 - 12), 
                    DateFinPeriodePee = new DateTime(2021 - 06 - 28), 
                    IdPee = 5, NumOrdre = 2 
                }, 
                new PeriodePee 
                { 
                    DateDebutPeriodePee = new DateTime(2021 - 04 - 12), 
                    DateFinPeriodePee = new DateTime(2021 - 06 - 28),
                    IdPee = 5, NumOrdre = 3 
                });
            _context.SaveChanges();
            return _context;
        }

    }
}
