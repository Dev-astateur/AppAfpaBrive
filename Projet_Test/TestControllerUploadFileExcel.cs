using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Projet_Test.InMemoryDb;
using System.Collections.Generic;

namespace Projet_Test
{
    class TestControllerUploadFileExcel
    {

        public DbContextMocker db = new DbContextMocker();
        AFPANADbContext dba;


        [SetUp]
        public void Setup()
        {
            ///Initialize db context in memory
            dba = db.GetAFPANADbContext("test");
            var s1 = new CollaborateurAfpa
            {
                NomCollaborateur = "Titi",
                MatriculeCollaborateurAfpa = "96AA011"
            };


            dba.CollaborateurAfpas.Add(s1);
            dba.SaveChanges();

            ///Initialize config mocker
          
        }


        [Test]
        public void TestControllerUploadFileExcel_ShouldReturnIndexView()
        {
            
        }
    }
}
