using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Test
{
    class TestAutorisationAbsence
    {
        public DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;
        //Faire une fausse configuration (iConfiguration)
        
     

        [Test]
        public void TestAction_CompleterInfoAbsence_DoitRetournerFichier()
        {
            // Mock de DBContext
            dba = db.GetAFPANADbContext("test");

            //Mock de Configuration
            var inMemorySettings = new Dictionary<string, string> {
    {"TopLevelKey", "TopLevelValue"},
    {"SectionName:SomeKey", "SectionValue"},
    //...populate as needed for the test
};

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //Mock de I Host Environement
           // var mockEnvironment = new Mock<IHostingEnvironment>();


            AutorisationAbsenceController autorisationAbsenceController = new AutorisationAbsenceController(dba, configuration,);
        }

    }
}
