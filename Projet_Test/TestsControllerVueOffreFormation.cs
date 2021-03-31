using NUnit.Framework;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web;
using AppAfpaBrive.Web.Controllers.Formateur;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projet_Test.InMemoryDb;
using System;
using AppAfpaBrive.Web.Layers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projet_Test
{
    public class TestsControllerVueOffreFormation
    {
        public DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public void TestListeStagiaireControllerRenvoieView()
        //{
        //    DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
        //    string path = Directory.GetCurrentDirectory();
            
        //    optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
        //    AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
        //    StagiaireParOffredeFormationController controller = new StagiaireParOffredeFormationController(contexte);
            
        //    var result = controller.ListeStagiaireParOffreFormation();

        //    Assert.IsInstanceOf<ViewResult>(result);

        //}
        //[Test]
        //public void TestListeStagiaireControllerRenvoieTypeStagiaire()
        //{
        //    DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
        //    string path = Directory.GetCurrentDirectory();

        //    optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
        //    AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
        //    StagiaireParOffredeFormationController controller = new StagiaireParOffredeFormationController(contexte);

        //    var result = controller.ListeStagiaireParOffreFormation();
        //    ViewResult view = result as ViewResult;
            
        //    Assert.IsInstanceOf<List<Beneficiaire>>(view.Model);
            
        //}
        [Test]
        [TestCase (1,1)]
        public async Task TestOffreDeFormationBeneficiaireControllerRenvoieView(int id, int? page)
        {
            //DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            //string path = Directory.GetCurrentDirectory();

            //optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            //AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            dba = db.GetAFPANADbContext("test");


            OffreDeFormationBeneficiaireController controller = new OffreDeFormationBeneficiaireController(dba);

            var view = await controller.OffredeFormationBeneficiaire(id, page);

            Assert.IsInstanceOf<ViewResult>(view);

        }
        [Test]
        [TestCase (1,1)]
        public async Task TestOffreDeFormationBeneficiaireControllerRenvoieOffreFormation(int id, int page)
        {
            //DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            //string path = Directory.GetCurrentDirectory();

            //optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            //AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            dba = db.GetAFPANADbContext("test");
            OffreDeFormationBeneficiaireController controller = new OffreDeFormationBeneficiaireController(dba);

            var view = await controller.OffredeFormationBeneficiaire(id, page);
            ViewResult result= view as ViewResult;

            Assert.IsInstanceOf<BeneficiaireSpecifiqueModelView>(result.Model);
        }
        
       
    }
}