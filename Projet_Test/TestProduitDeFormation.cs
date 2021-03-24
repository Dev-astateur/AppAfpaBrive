﻿using NUnit.Framework;
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

namespace Projet_Test
{
    [TestFixture]
    public class TestProduitDeFormation
    {
        DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;

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

        [Test]
        
        public void TestMethodeDeleteValide()
        {
            
            dba = db.GetAFPANADbContext("bloub");

            var bloub = new ProduitFormation
            {
                CodeProduitFormation = 7,
                NiveauFormation = "3",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpoakfjha"
            };
            dba.ProduitFormations.Add(bloub);

            dba.SaveChanges();
            dba.Entry<ProduitFormation>(bloub).State =EntityState.Detached;

            ProduitFormationController controleur = new ProduitFormationController(dba);
            var view = controleur.Delete(7);
            
            var result = dba.ProduitFormations.Where(x=> x.CodeProduitFormation==7);
            Assert.IsTrue(result.Count()==0);
        }

        [Test]
        public void TestMethodeCreateValide()
        {
            dba = db.GetAFPANADbContext("xxx");
            var bloub = new ProduitFormationModelView
            {
                CodeProduitFormation = 7,
                NiveauFormation = "3",
                LibelleCourtFormation = "abc",
                LibelleProduitFormation = "dkazkdakanfpoakfjha"
            };
            var item = bloub.GetProduitFormation();
           
            dba.Entry<ProduitFormation>(item).State = EntityState.Detached;

            ProduitFormationController controleur = new ProduitFormationController(dba);

            var view = controleur.Create(bloub);

            var result = dba.ProduitFormations.Where(x => x.CodeProduitFormation == 7);
            Assert.IsTrue(result.Count() == 1);
        }
        
    }
}
