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
    class TestDuEntreprisePartial
    {
        [SetUp]
        public void Setup ()
        {

        }

        [Test]
        public void Test_Passage_Des_Données_A_La_Vue_True()
        {
            DbContextOptionsBuilder<AFPANADbContext> builder = new DbContextOptionsBuilder<AFPANADbContext>();
            builder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;", assembly=>assembly.MigrationsAssembly(typeof(AFPANADbContext).Assembly.FullName));


            PeeController controleur = new PeeController(new AFPANADbContext(builder.Options));
            var view = controleur.ValidationStage();

            Assert.IsInstanceOf<ViewResult>(view);

        }
    }
}
