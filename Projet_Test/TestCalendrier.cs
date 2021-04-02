using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers.Calendrier;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    public class TestCalendrier
    {
        private readonly AFPANADbContext db = DbContextMocker.GetAFPANADbContext("test");
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(1,1)]
        public void TestEvenementControllerRenvoieViewIndex (int month, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Index(month, year);

            Assert.IsInstanceOf<ViewResult>(view);

        }

        [Test]
        [TestCase(1, 1)]
        public void TestEvenementControllerRenvoieModelViewIndex (int month, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Index(month, year);
            ViewResult result = view as ViewResult;

            Assert.IsInstanceOf<EvenementModelView>(result.Model);

        }
        [Test]
        [TestCase("mars", 1)]
        public void TestEvenementControllerRenvoieViewPrecedent(string precedent, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Precedent(precedent, year);

            Assert.IsInstanceOf<ViewResult>(view);

        }
        [Test]
        [TestCase("mars", 1)]
        public void TestEvenementControllerRenvoieModelViewPrecedent(string precedent, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Precedent(precedent, year);
            ViewResult result = view as ViewResult;

            Assert.IsInstanceOf<EvenementModelView>(result.Model);

        }
        [Test]
        [TestCase("mars", 1)]
        public void TestEvenementControllerRenvoieViewSuivant(string suivant, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Suivant(suivant, year);

            Assert.IsInstanceOf<ViewResult>(view);

        }
        [Test]
        [TestCase("mars", 1)]
        public void TestEvenementControllerRenvoieModelViewSuivant(string suivant, int year)
        {
            EvenementController controller = new EvenementController(db);

            var view = controller.Precedent(suivant, year);
            ViewResult result = view as ViewResult;

            Assert.IsInstanceOf<EvenementModelView>(result.Model);

        }


    }



}
