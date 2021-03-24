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


namespace Projet_Test
{
    public class TestsControllerVueOffreFormation
    {
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
        public void TestOffreDeFormationBeneficiaireControllerRenvoieView()
        {
            DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            string path = Directory.GetCurrentDirectory();

            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            OffreDeFormationBeneficiaireController controller = new OffreDeFormationBeneficiaireController(contexte);

            //var result = controller.OffreDeFormationBeneficiaire();

            //Assert.IsInstanceOf<ViewResult>(result);

        }
        [Test]
        public void TestOffreDeFormationBeneficiaireControllerRenvoieOffreFormation()
        {
            DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            string path = Directory.GetCurrentDirectory();

            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            OffreDeFormationBeneficiaireController controller = new OffreDeFormationBeneficiaireController(contexte);

            //var result = controller.OffreDeFormationBeneficiaire();
            //ViewResult view = result as ViewResult;

           // Assert.IsInstanceOf<OffreFormationModelView>(view.Model);
        }
    }
}