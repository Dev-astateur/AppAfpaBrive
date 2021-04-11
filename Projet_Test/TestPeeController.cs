using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using System.IO;
using Microsoft.Extensions.Hosting;
using AppAfpaBrive.Web.Layers;
using Moq;
using Projet_Test.PeeTestInMemoryDb;
using AppAfpaBrive.Web.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Projet_Test.IMailSenderMock;

namespace Projet_Test
{
    [TestFixture]
    public class TestPeeController
    {

        AFPANADbContext Context;
        PeeDocumentTest peeDocumentTest = new PeeDocumentTest();
        Mock<IHostEnvironment> mockEnvironment;
        Mock<IConfiguration> IConfigMok;
        List<string> listFilesMok = new List<string>();
        ImpressionFicheSuivi ficheSuivi;
        MailSenderMock mail = new MailSenderMock();

        [SetUp]
        public void LoadContext()
        {
            Context = peeDocumentTest.GetPee();
            mockEnvironment = new Mock<IHostEnvironment>();
            mockEnvironment.Setup(m => m.ContentRootPath).Returns(@"C:\Users\CDA\Desktop\ProjetPee_V5\ProjetPEE\AppAfpaBrive.Web\");
            IConfigMok = new Mock<IConfiguration>();
            IConfigMok.SetupAllProperties();

            ficheSuivi = new ImpressionFicheSuivi(Context, mockEnvironment.Object);
        }




        [Test]
        public async Task ListPeeBeneficiaire_ReturnValideCountList()
        {
            //Arrange



            var controller = new PeeLayer(Context);
            //Act 
            IEnumerable<Pee> result = await controller.GetPeeEntrepriseWithBeneficiaireBy(20101, "19011");

            //Assert

            Assert.AreEqual(2, result.Count());

        }
        [Test]
        public async Task ListPeriodePeeIsValide()
        {
            //Arrange

            var controller = new PeeLayer(Context);
            //Act 
            IEnumerable<PeriodePee> periodePees = await controller.GetListPeriodePeeByIdPee(20101, "19011");
            //Assert
            Assert.AreEqual(2, periodePees.Count());
        }

        [Test]
        public async Task tesGetBenenificiare()
        {
            //Arrange

            Pee pee = new Pee();
            //Act
            pee = await ficheSuivi.GetDataBeneficiairePeeById(4);
            //Assert
            Assert.IsTrue(pee.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire.Contains("16174318"));

        }
        [Test]
        [TestCase(5)]
        public async Task testGetOffreFormation(int id)
        {
            //Arrange
            OffreFormation offreFormation = new OffreFormation();
            //Act
            offreFormation = await ficheSuivi.GetEntrepriseProfessionnel(id);
            //Assert
            Assert.AreEqual(20101, offreFormation.IdOffreFormation);
        }

        [Test]
        [TestCase(6)]
        public async Task TestGetEntrepriseProfessionnelData(int id)
        {
            //Arrange
            EntrepriseProfessionnel entrepriseProfessionnel = new EntrepriseProfessionnel();
            //Act 
            entrepriseProfessionnel = await ficheSuivi.GetEntrepriseProfessionnelData(id);
            //Assert
            Assert.AreSame("Domme", entrepriseProfessionnel.IdProfessionnelNavigation.NomProfessionnel);
        }
        [Test]
        [TestCase(4)]
        public async Task TestGetPeriodePeeByPeeId(int id)
        {
            //Arrange
            PeriodePee periodePee = new PeriodePee();
            DateTime dateTime = new DateTime(2021, 04, 12);
            //Act
            periodePee = await ficheSuivi.GetPeriodePeeByPeeId(id);
            //Assert
            Assert.That(dateTime, Is.EqualTo(periodePee.DateDebutPeriodePee));

        }
        [Test]
        [TestCase(1, 4)]
        public async Task TestpathFileModelFollowUpDocumentPee_ReturnFileConventionMasculinExist(int value, int id)
        {
            //Arrange
            listFilesMok.Add(Path.Combine(mockEnvironment.Object.ContentRootPath, @"ModelesOffice\2-Lettre_Envoi_Convention.docx"));
            listFilesMok.Add(Path.Combine(mockEnvironment.Object.ContentRootPath, @"ModelesOffice\1-ConventionPE_M.docx"));
            //Act
            var listPathMethode = await ficheSuivi.pathFileModelFollowUpDocumentPee(value, id);
            //Assert
            Assert.AreEqual(listFilesMok.Contains(@"~\ModelesOffice\1-ConventionPE_M.docx"), listPathMethode.Contains(@"~\ModelesOffice\1-ConventionPE_M.docx"));

        }

        [Test]
        [TestCase(6, 1)]
        public async Task TestGetPathFile_ReturnFilesIsCopied(int id, int value)
        {
            //Arrange
            ficheSuivi = new ImpressionFicheSuivi(Context, mockEnvironment.Object);
            //Act
            listFilesMok = await ficheSuivi.GetPathFile(id, value);
            //Assert
            Assert.IsNotNull(listFilesMok);
            listFilesMok.ForEach(File.Delete);

        }
        [Test]
        public void TestPeeControllerIndex_ReturnViewEmpty()
        {
            //arrange

            var controller = new PeeController(Context, IConfigMok.Object, mockEnvironment.Object,mail);
            //Act
            var result = controller.Index();
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);

        }
        [Test]
        [TestCase(20101, "19011")]
        public async Task TestPeeContriller_ViewAfficheBeneficiaire(int IdOffreFormation, string IdEtablissement)
        {
            //Arrange
            var controller = new PeeController(Context, IConfigMok.Object, mockEnvironment.Object,mail);
            //Act
            var result = await controller.AfficheBeneficiairePee(IdOffreFormation, IdEtablissement) as ViewResult;
            //Assert
            Assert.IsTrue(result.ViewData["ListPeeSansDoublons"] != null);


        }



    }
}
