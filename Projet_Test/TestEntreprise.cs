using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
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
        [SetUp]
        public void setup()
        {
        }
        [Test]
        public void TestActionListeEntrepriseRenvoiePagingListEntreprise()
        {
            //DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            //string path = Directory.GetCurrentDirectory();

            //optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            //AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
           
               dba= db.GetAFPANADbContext("test");

            EntrepriseController controller = new EntrepriseController(dba);

            var result = controller.ListeEntreprise("","",1);
            ViewResult view = result as ViewResult;

            Assert.IsInstanceOf<PagingList<Entreprise>>(view.Model);
        }

        [Test]
        public void TestActionListeEntreprisePourModifRenvoieView()
        {
            //DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            //string path = Directory.GetCurrentDirectory();

            //optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            //AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);

            dba = db.GetAFPANADbContext("test");

            EntrepriseController controller = new EntrepriseController(dba);

            var result = controller.ListeEntreprise("", "", 1);
            ViewResult view = result as ViewResult;

            Assert.IsInstanceOf<PagingList<Entreprise>>(view.Model);
        }

        [Test]  
        public void TestInsertionEntrepriseOK()
        {
            Entreprise entreprise = new Entreprise
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
        public void TestInsertionEntrepriseRaisonSocialeAbsente()
        {
            Entreprise entreprise = new Entreprise
            {
               
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
    }
}
