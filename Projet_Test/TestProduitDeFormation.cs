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

namespace Projet_Test
{
    [TestFixture]
    public class TestProduitDeFormation
    {
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


            ProduitFormationController controleur = new ProduitFormationController(new AFPANADbContext(builder.Options));
            var view = await controleur.Index("concepteur",1, "CodeProduitFormation");

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        [TestCase(4)]
        public void TestViewEditionValide(int id)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

            ProduitFormationController controleur = new ProduitFormationController(new AFPANADbContext(builder.Options));
            var view = controleur.Edit(id);
            Assert.IsInstanceOf<ViewResult>(view);
        }

        //[Test]
        //[TestCase(6)]
        //public void TestViewDeleteValide(int id)
        //{
        //    DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
        //    builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

        //    ProduitFormationController controleur = new ProduitFormationController(new AFPANADbContext(builder.Options));
        //    var view = controleur.Delete(id);
        //    Assert.IsInstanceOf<ViewResult>(view);
        //}

    }
}
