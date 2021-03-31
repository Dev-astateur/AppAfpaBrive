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

        private readonly AFPANADbContext db = DbContextMocker.GetAFPANADbContext("test");        


        [SetUp]
        public void Setup()
        {
            ///Initialize db context in memory
            var s1 = new CollaborateurAfpa
            {
                NomCollaborateur = "Titi",
                MatriculeCollaborateurAfpa = "96AA011"
            };


            db.CollaborateurAfpas.Add(s1);
            db.SaveChanges();

            ///Initialize config mocker
          
        }


        [Test]
        public void TestControllerUploadFileExcel_ShouldReturnIndexView()
        {
            
        }
    }
}
