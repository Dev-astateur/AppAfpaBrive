using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    
   public class TestEntreprise
    {
        public DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;
        //[SetUp]
        //public void setup()
        //{
        //}
        [Test]
        public void TestActionListeEntrepriseRenvoiePagingListEntreprise()
        {          
               dba= db.GetAFPANADbContext("test");

            EntrepriseController controller = new EntrepriseController(dba);

            var result = controller.ListeEntreprise("","",1);
            ViewResult view = result as ViewResult;

            Assert.IsInstanceOf<PagingList<Entreprise>>(view.Model);
        }
      

        [Test]
        public void Test_Action_Liste_Entreprise_Pour_Modif_DoitRenvoyer_PagingListEntreprise()
        {
            dba = db.GetAFPANADbContext("test");

            EntrepriseController controller = new EntrepriseController(dba);

            var result = controller.ListeEntreprise("", "", 1);
            ViewResult view = result as ViewResult;

            Assert.IsInstanceOf<PagingList<Entreprise>>(view.Model);
        }

        [Test]  
        public void TestInsertion_EntrepriseValide_NeDoitpasRenvoyerDerreur()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale = "Xiaomi",
                NumeroSiret = "42159769100029",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal="75000",
                Ville="Paris",
                Idpays2="FR"
            };

            Assert.IsFalse(ValidationService.ValidateModel(entreprise).Any
                (e => e.MemberNames.Contains("RaisonSociale")
                 && e.ErrorMessage.Contains("sociale")));
        }

        [Test]
        public void TestInsertionEntrepriseNotOK()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale = "Xiaomi",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal = "75000",
                Ville = "Paris",
                Idpays2 = "FR"
            };

            Assert.IsTrue(ValidationService.ValidateModel(entreprise).Any
                (e => e.MemberNames.Contains("NumeroSiret")
                 && e.ErrorMessage.Contains("sociale")));
        }

        [Test]
        public void Test_InsertionEntreprise_RaisonSocialeAbsente_DoitRenvoyerErreur()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale="",
                NumeroSiret = "42159769100029",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal = "75000",
                Ville = "Paris",
                Idpays2 = "FR"
            };

            Assert.IsTrue(ValidationService.ValidateModel(entreprise).Any
                (e => e.MemberNames.Contains("RaisonSociale")
                 && e.ErrorMessage.Contains("sociale")));
        }

        [Test]
        public void Test_CodePostalAbsentAbsent_Insertion_Doit_RetournerErreur()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale = "Xiaomi",
                NumeroSiret = "42159769100029",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal = "",
                Ville = "Paris",
                Idpays2 = "FR"
            };
            Assert.IsTrue(ValidationService.ValidateModel(entreprise).Any
                 (e => e.MemberNames.Contains("CodePostal")
                  && e.ErrorMessage.Contains("code")));
        }

        [Test]
        public void Test_IdPAYS_Absent_doitRenvoyerErreur()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale = "Xiaomi",
                NumeroSiret = "42159769100029",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal = "75000",
                Ville = "Paris",
                Idpays2 = ""
            };
            Assert.IsTrue(ValidationService.ValidateModel(entreprise).Any
                 (e => e.MemberNames.Contains("Idpays2")
                  && e.ErrorMessage.Contains("pays")));
        }

        [Test]
        public void Test_Numero_Siret_Invalide_doit_retournerMessageErreur()
        {
            EntrepriseListViewModel entreprise = new EntrepriseListViewModel
            {
                RaisonSociale = "Xiaomi",
                NumeroSiret = "111111111111111",
                Ligne1Adresse = "13, Avenue Charles De Gaulle",
                CodePostal = "75000",
                Ville = "Paris",
                Idpays2 = "FR"
            };
            Assert.IsTrue(ValidationService.ValidateModel(entreprise).Any
                 (e => e.ErrorMessage.Contains("Siret")));
           
        }

        //[Test]
        //public void Test_layer_GetAllEntrepriseForPaging()
        //{
        //    Entreprise entreprise = new Entreprise
        //    {
        //        RaisonSociale = "Xiaomi",
        //        NumeroSiret = "42159769100029",
        //        Ligne1Adresse = "13, Avenue Charles De Gaulle",
        //        CodePostal = "75000",
        //        Ville = "Paris",
        //        Idpays2 = "FR"
        //    };
        //    List<Entreprise> liste = new List<Entreprise>();
        //    liste.Add(entreprise);
        //    PagingList<Entreprise> pagingList = new PagingList<Entreprise>(liste);

        //    dba = db.GetAFPANADbContext("test");
        //    EntrepriseLayer layer = new EntrepriseLayer(dba);
        //  var Result= layer.GetAllEntrepriseForPaging();
        //    Assert.AreEqual(Result,entreprise);

        //}


    }
}
