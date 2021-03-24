using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    [TestFixture]
    class TestEntreprise
    {
        //[Test]
        //public void TestActionCreerEntrepriseRenvoieView()
        //{
        //    DbContextOptionsBuilder<AFPANADbContext> optionsBuilder = new DbContextOptionsBuilder<AFPANADbContext>();
        //    string path = Directory.GetCurrentDirectory();

        //    optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
        //    AFPANADbContext contexte = new AFPANADbContext(optionsBuilder.Options);
        //    EntrepriseController controller = new EntrepriseController(contexte);

        //    var result = controller.CreerEntreprise();
        //    ViewResult view = result as ViewResult;

        //    Assert.IsInstanceOf<Entreprise>(view.Model);
        //}
        [TestCase]
        
        public void TestInsertionEntreprise()
        {
            

        }

    }
}
