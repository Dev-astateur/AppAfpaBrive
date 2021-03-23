using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Controllers;
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
    class TestControllerUploadFileExcel
    {

        public DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;
        

        [SetUp]
        public void Setup()
        {
            dba = db.GetAFPANADbContext("test");
            var s1 = new CollaborateurAfpa
            {
                NomCollaborateur = "Titi",
                MatriculeCollaborateurAfpa = "96AA011"
            };


            dba.CollaborateurAfpas.Add(s1);
            dba.SaveChanges();
           
    }


        [Test]
        public void TestControllerUploadFileExcel_ShouldReturnIndexView()
        {
            var inMemorySettings = new Dictionary<string, string>
            { };

            //FileUploadController controller = new FileUploadController(dba,);

        }
    }
}
