using AppAfpaBrive.DAL;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using AppAfpaBrive.Web.Layers.StatsLayer;
using AppAfpaBrive.BOL;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AppAfpaBrive.Web.Layers;

namespace Projet_Test
{
    public class TestInsertionAggregatStats
    {
        private AFPANADbContext dba = DbContextMocker.GetAFPANADbContext("blob");


        [SetUp]
        public void Setup()
        {

             dba = DbContextMocker.GetAFPANADbContext("blob");
          //  dba.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        #region Test Creation de ligne
        [Test]
        public void TestCreateNewLine()
        {
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9952,
                Annee = 2020
            };

            objCreate.CreateNewLine(newLine);
            Assert.IsTrue(
                dba.InsertionTroisMois.Count() == 2 &&
                dba.InsertionSixMois.Count() == 2 &&
                dba.InsertionDouzeMois.Count() == 2
                );
        }

        [Test]
        public void TestCreateNewLineDoublon()
        {
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9952,
                Annee = 2020
            };
            try
            {
                objCreate.CreateNewLine(newLine);
                objCreate.CreateNewLine(newLine);
                Assert.Fail();
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Test de récupération des datas par table
        [Test]
        public void TestGetInsertion3MoisData()
        {
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9955,
                Annee = 2020,
                Cdi = 10
            };
            objCreate.CreateNewLine(newLine);
            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            InsertionsTroisMois insertionTroisMoisData = (InsertionsTroisMois)data.GetInsertionTroisMois().Where(x => x.IdOffreFormation == 9955).First();
            Assert.IsTrue(insertionTroisMoisData.Cdi == 10);
        }
        [Test]
        public void TestGetInsertion6MoisData()
        {
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9953,
                Annee = 2020,
                Cdi = 10
            };
            objCreate.CreateNewLine(newLine);
            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            InsertionsSixMois insertionSixMoisData = (InsertionsSixMois)data.GetInsertionSixMois().Where(x => x.IdOffreFormation == 9953).First();
            Assert.IsTrue(insertionSixMoisData.Cdi == 10);
        }
        [Test]
        public void TestGetInsertion12MoisData()
        {
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9954,
                Annee = 2020,
                Cdi = 10
            };
            objCreate.CreateNewLine(newLine);
            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            InsertionsDouzeMois insertionDouzeMoisData = (InsertionsDouzeMois)data.GetInsertionDouzeMois().Where(x => x.IdOffreFormation == 9954).First();
            Assert.IsTrue(insertionDouzeMoisData.Cdi == 10);
        }
        #endregion
        #region Test recupération de data liée a une année, formation ou etablissement précis.
        [Test]
        public void TestGetOneYearData()
        {
            List<IInsertion> itms = new List<IInsertion>();
            itms.Add(new InsertionsTroisMois { Annee = 2020 });
            itms.Add(new InsertionsTroisMois { Annee = 2019 });

            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            List<IInsertion> toCompare = data.GetOneYearData(itms, 2020);
            Assert.IsTrue(toCompare.Count == 1);
        }
        [Test]
        public void TestGetOneFormationData()
        {
            List<IInsertion> itms = new List<IInsertion>();
            itms.Add(new InsertionsTroisMois { IdOffreFormation = 2000 });
            itms.Add(new InsertionsTroisMois { IdOffreFormation = 2001 });

            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            List<IInsertion> test = data.GetOneFormationData(itms, 2000);
            Assert.IsTrue(test.Count == 1);
        }
        [Test]
        public void TestGetOneEtablissementData()
        {
            List<IInsertion> itms = new List<IInsertion>();
            itms.Add(new InsertionsTroisMois { IdEtablissement = "19000" });
            itms.Add(new InsertionsTroisMois { IdEtablissement = "19001" });
            GetInsertionDataLayer data = new GetInsertionDataLayer(dba);
            List<IInsertion> test = data.GetOneEtablissementData(itms, "19000");
            Assert.IsTrue(test.Count == 1);
        }
        #endregion
        #region Test insertion d'une réponse (maj des tables)
        [Test]
        public void TestInsertion3Mois()
        {
            InsertInsertionDataLayer objInsert = new InsertInsertionDataLayer(dba);
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);
          
            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19011",
                IdOffreFormation = 9949,
                Annee = 2021
            };
            objCreate.CreateNewLine(newLine);

            DestinataireEnquete enquete = new DestinataireEnquete
            {
                EnEmploi = true,
                IdEtablissement = "19011",
                IdOffreFormation = 9949,
                MatriculeBeneficiaire = "11aaa11",         
            };
            enquete.IdContratNavigation = new Contrat { IdContrat = 1, EnLienMetierFormation = true, TypeContrat = 1 };
            enquete.IdPlanificationCampagneMailNavigation = new PlanificationCampagneMail { Type = "1" };
            enquete.DateRealisationEnquete = DateTime.Now;

            dba.OffreFormations.Add(new OffreFormation
            {
                IdOffreFormation = 9949,
                IdEtablissement = "19011",
                DateFinOffreFormation = DateTime.Now
            });
            dba.SaveChanges();
            
            objInsert.InsertAnwser(enquete);
            
            int x = dba.InsertionTroisMois
                .Where(x => x.IdEtablissement == "19011" && x.EnLienAvecFormation == true && x.IdOffreFormation == 9949)
                .Select(x => x.Cdi).First();

            Assert.AreEqual(1, x);
        }

        [Test]
        public void TestInsertion6Mois()
        {
            InsertInsertionDataLayer objInsert = new InsertInsertionDataLayer(dba);
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);

            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19013",
                IdOffreFormation = 9950,
                Annee = 2021
            };

            objCreate.CreateNewLine(newLine);

            DestinataireEnquete enquete = new DestinataireEnquete
            {
                EnEmploi = true,
                IdEtablissement = "19013",
                IdOffreFormation = 9950,
                MatriculeBeneficiaire = "11aaa11",
            };
            enquete.IdContratNavigation = new Contrat { IdContrat = 1, EnLienMetierFormation = true, TypeContrat = 1 };
            enquete.IdPlanificationCampagneMailNavigation = new PlanificationCampagneMail { Type = "2" };
            enquete.DateRealisationEnquete = DateTime.Now;

            dba.OffreFormations.Add(new OffreFormation
            {
                IdOffreFormation = 9950,
                IdEtablissement = "19013",
                DateFinOffreFormation = DateTime.Now
            });
            dba.SaveChanges();

            objInsert.InsertAnwser(enquete);

            int x = dba.InsertionSixMois
                .Where(x => x.IdEtablissement == "19013" && x.EnLienAvecFormation == true && x.IdOffreFormation == 9950)
                .Select(x => x.Cdi).First();

            Assert.AreEqual(1, x);
        }

        [Test]
        public void TestInsertionDouzeMois()
        {
            InsertInsertionDataLayer objInsert = new InsertInsertionDataLayer(dba);
            CreateNewLineDataLayer objCreate = new CreateNewLineDataLayer(dba);

            NewLineStats newLine = new NewLineStats
            {
                IdEtablissement = "19012",
                IdOffreFormation = 9952,
                Annee = 2021
            };

            objCreate.CreateNewLine(newLine);

            DestinataireEnquete enquete = new DestinataireEnquete
            {
                EnEmploi = true,
                IdEtablissement = "19012",
                IdOffreFormation = 9952,
                MatriculeBeneficiaire = "11aaa11",
            };
            enquete.IdContratNavigation = new Contrat { IdContrat = 1, EnLienMetierFormation = true, TypeContrat = 1 };
            enquete.IdPlanificationCampagneMailNavigation = new PlanificationCampagneMail { Type = "3" };
            enquete.DateRealisationEnquete = DateTime.Now;

            dba.OffreFormations.Add(new OffreFormation
            {
                IdOffreFormation = 9952,
                IdEtablissement = "19012",
                DateFinOffreFormation = DateTime.Now
            });
            dba.SaveChanges();

            objInsert.InsertAnwser(enquete);

            int x = dba.InsertionDouzeMois
                .Where(x => x.IdEtablissement == "19012" && x.EnLienAvecFormation == true && x.IdOffreFormation == 9952)
                .Select(x => x.Cdi).First();

            Assert.AreEqual(1, x);
        }
        #endregion
    }
}
