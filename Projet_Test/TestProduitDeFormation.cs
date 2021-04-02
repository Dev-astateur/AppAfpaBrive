using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using AppAfpaBrive.BOL;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.Web.Controllers.ProduitFormation;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Projet_Test.InMemoryDb;
using Microsoft.Extensions.Logging;
using Moq;

namespace Projet_Test
{
    [TestFixture]
    public class TestProduitDeFormation
    {
        private  AFPANADbContext db = DbContextMocker.GetAFPANADbContext("AFPANA");
        private readonly ILogger<ProduitFormationController> _logger = Mock.Of<ILogger<ProduitFormationController>>();
        [SetUp]
        public void Setup()
        {
            db = DbContextMocker.GetAFPANADbContext("AFPANA");
        }


        [Test]
        public void LibelleCourtFormationTropLong()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation="3",
                LibelleProduitFormation = "azdfazddzdzdzd",
                LibelleCourtFormation = "ABCDEFSDFZSS"
            };
            Assert.IsTrue(ValidationService.ValidateModel(produit).Any(va => 
            va.MemberNames.Contains("LibelleCourtFormation")
            && va.ErrorMessage.Contains("Le Libelle Court Formation")));
        }

        [Test]
        public void LibelleCourtFormationValide()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation = "3",
                LibelleProduitFormation="zdzd",
                LibelleCourtFormation = "azerazerzc"
            };
            Assert.IsFalse(ValidationService.ValidateModel(produit).Any
                (va => va.MemberNames.Contains("LibelleCourtFormation")
                && va.ErrorMessage.Contains("Le Libelle Court Formation")));
        }
        
        [Test]
        public void LibelleFormationAbsent()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation = "3",
                LibelleCourtFormation = "azerazerzc"
            };
            Assert.IsTrue(ValidationService.ValidateModel(produit).Any
                (va => va.MemberNames.Contains("LibelleProduitFormation")
                && va.ErrorMessage.Contains("Le libelle du produit")));
        }
        [Test]
        public void LibelleFormationPresent()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation = "3",
                LibelleProduitFormation = "zdzd",
                LibelleCourtFormation = "azerazerzc"
            };
            Assert.IsFalse(ValidationService.ValidateModel(produit).Any
                (va => va.MemberNames.Contains("LibelleProduitFormation")
                && va.ErrorMessage.Contains("Le libelle du produit")));
        }

        [Test]
        public void CodeProduitFormationPresent()
        {
            ProduitFormationModelView produit = new ProduitFormationModelView
            {
                CodeProduitFormation = 6,
                NiveauFormation = "3",
                LibelleProduitFormation = "zdzd",
                LibelleCourtFormation = "azerazerzc"
            };
            Assert.IsFalse(ValidationService.ValidateModel(produit).Any
                (va => va.MemberNames.Contains("CodeProduitFormation")
                && va.ErrorMessage.Contains("Le code produit formation")));
        }

        // dur a tester étant donné que int n'est pas nullable et donc CodeproduitFormation
        //prend naturellement 0 comme valeur quand non fourni
        //[Test]
        //public void CodeProduitFormationAbsent()
        //{
        //    ProduitFormationModelView produit = new ProduitFormationModelView
        //    {
        //        NiveauFormation = "3",
        //        LibelleProduitFormation = "zdzd",
        //        LibelleCourtFormation = "azerazerzc"
        //    };
        //    Assert.IsTrue(ValidationService.ValidateModel(produit).Any
        //        (va => va.MemberNames.Contains("CodeProduitFormation")
        //        && va.ErrorMessage.Contains("Le code produit formation")));
        //}

        [Test]
        [TestCase("concepteur",1, "CodeProduitFormation")]
        public async Task Test_Creation_De_La_Vue_True(string b, int page, string c)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

            ProduitFormationController controleur = new ProduitFormationController(db,_logger);
            var view = await controleur.Index("concepteur",1, "CodeProduitFormation");

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        
        public void TestMethodeDeleteValide()
        {

            var produitformation = new ProduitFormation
            {
                CodeProduitFormation = 7,
                NiveauFormation = "3",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpoakfjha"
            };
            db.ProduitFormations.Add(produitformation);

            db.SaveChanges();
            db.Entry<ProduitFormation>(produitformation).State =EntityState.Detached;

            ProduitFormationController controleur = new ProduitFormationController(db,_logger);
            var view = controleur.Delete(7);
            
            var result = db.ProduitFormations.Where(x=> x.CodeProduitFormation==7);
            Assert.IsTrue(result.Count()==0);
        }

        [Test]
        public void TestMethodeCreateValide()
        {
            var produitformation = new ProduitFormationModelView
            {
                CodeProduitFormation = 7,
                NiveauFormation = "3",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpa"
            };
            var item = produitformation.GetProduitFormation();
           
            db.Entry<ProduitFormation>(item).State = EntityState.Detached;

            ProduitFormationController controleur = new ProduitFormationController(db,_logger);

            var view = controleur.Create(produitformation);

            var result = db.ProduitFormations.Where(x => x.CodeProduitFormation == 7);
            Assert.IsTrue(result.Count() == 1);
        }
        
        [Test]
        public void NiveauFormationTropLong()
        {
            var produitformation = new ProduitFormationModelView
            {
                CodeProduitFormation = 7,
                NiveauFormation = "345687",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpoakfjha"
            };
           Assert.IsTrue(ValidationService.ValidateModel(produitformation).Any(va =>
           va.MemberNames.Contains("NiveauFormation")
           && va.ErrorMessage.Contains("Le Niveau de Formation ne peut pas etre plus long que 5 caracteres")));
        }
        [Test]
        public void NiveauFormationValide()
        {
            var produitformation = new ProduitFormationModelView
            {
                CodeProduitFormation = 7,
                NiveauFormation = "3",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpoakfjha"
            };
            Assert.IsFalse(ValidationService.ValidateModel(produitformation).Any(va =>
            va.MemberNames.Contains("NiveauFormation")
            && va.ErrorMessage.Contains("Le Niveau de Formation ne peut pas etre plus long que 5 caracteres")));
        }
    }
}
