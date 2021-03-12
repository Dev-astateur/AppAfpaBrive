using NUnit.Framework;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web;
using AppAfpaBrive.Web.Controllers.Formateur;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Projet_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestListeStagiaireControllerRenvoieView()
        {
            DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            string path = Directory.GetCurrentDirectory();
            
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            StagiaireParOffredeFormationController controller = new StagiaireParOffredeFormationController(contexte);
            
            var result = controller.ListeStagiaireParOffreFormation();

            Assert.IsInstanceOf<ViewResult>(result);

        }
        [Test]
        public void TestListeStagiaireControllerRenvoieTypeStagiaire()
        {
            DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
            string path = Directory.GetCurrentDirectory();

            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
            StagiaireParOffredeFormationController controller = new StagiaireParOffredeFormationController(contexte);

            var result = controller.ListeStagiaireParOffreFormation();
            ViewResult view = result as ViewResult;
            
            Assert.IsInstanceOf<List<Beneficiaire>>(view.Model);
            
        }
    }
}