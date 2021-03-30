using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.Controllers;
using Microsoft.Extensions.Hosting;
using AppAfpaBrive.Web.Layers;
using Moq;
using Projet_Test.PeeTestInMemoryDb;
using Projet_Test.InMemoryDb;

namespace Projet_Test
{
    public class testControllerPee
    {
        DbContextMocker dbContextMocker = new DbContextMocker();
        AFPANADbContext _context;
        PeeDocumentTest peeDocumentTest;

        public AFPANADbContext Context { get => _context= peeDocumentTest.GetPee();  }

        [Test]
        public void ListPeeBeneficiaireIsValide()
        {
            //Range
            peeDocumentTest = new PeeDocumentTest(dbContextMocker);
            
            var controller = new PeeLayer(Context);
            //Act 
            IEnumerable<Pee> result = controller.GetPeeEntrepriseWithBeneficiaireBy(20101, "19011");

            //Assert

            Assert.AreEqual(3, result.Count());

        }
        [Test]
        public void ListPeriodePeeIsValide()
        {
            //Range
            peeDocumentTest = new PeeDocumentTest(dbContextMocker);
             var controller = new PeeLayer(Context);
            //Act 
            IEnumerable<PeriodePee> periodePees = controller.GetListPeriodePeeByIdPee(20101, "19011");
            Assert.AreEqual(3, periodePees.Count());
        }

        [Test]
        public void tesGetBenenificiare()
        {
            
            
        }

    }
}
