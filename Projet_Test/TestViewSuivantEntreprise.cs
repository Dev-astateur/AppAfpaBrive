using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Projet_Test
{
    class TestViewSuivantEntreprise
    {
        [SetUp]
        public void Setup ()
        {

        }

        [Test]
        [TestCase(7)]
        public void Test_Creation_De_La_Vue_True(int idPee)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly=>assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));


            PeeController controleur = new PeeController(new AFPANADbContext(builder.Options));
            var view = controleur.SuivantEntreprise(idPee);

            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        [TestCase(7)]
        public void Test_Sur_Data_Passe_A_La_Vue_True (int idPee)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

            PeeController controleur = new PeeController(new AFPANADbContext(builder.Options));
            var view = controleur.SuivantEntreprise(idPee);
            ViewResult viewResult = view as ViewResult;
            Assert.IsInstanceOf<AppAfpaBrive.Web.ModelView.PeeModelView>(viewResult.Model);
        }

        [Test]
        [TestCase(7)]
        public void Test_Sur_Data_Entreprise_A_La_Vue_True (int idPee)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

            PeeController controleur = new PeeController(new AFPANADbContext(builder.Options));
            var view = controleur.SuivantEntreprise(idPee);
            ViewResult viewResult = view as ViewResult;
            AppAfpaBrive.Web.ModelView.PeeModelView pee = viewResult.Model as AppAfpaBrive.Web.ModelView.PeeModelView;
            //Assert.IsInstanceOf<AppAfpaBrive.BOL.Entreprise>(pee.IdEntrepriseNavigation);
        }

        [Test]
        [TestCase(1)]
        public void Test_Sur_Data_Entreprise_A_La_Vue_IdPee_Inconnu(int idPee)
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly => assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));

            PeeController controleur = new PeeController(new AFPANADbContext(builder.Options));
            var view = controleur.SuivantEntreprise(idPee);
            ViewResult viewResult = view as ViewResult;
            AppAfpaBrive.Web.ModelView.PeeModelView pee = viewResult.Model as AppAfpaBrive.Web.ModelView.PeeModelView;
            Assert.That(pee.IdPee == 0);
        }
    }
}
