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
using AppAfpaBrive.Web.Controllers;
using Microsoft.Extensions.Hosting;
using AppAfpaBrive.Web.Layers;
using Projet_Test.InMemoryDb;
using NUnit.Framework.Internal;
using Projet_Test.IHostEnvironnementMock;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using ReflectionIT.Mvc.Paging;
using System.Net;
using Projet_Test.Utilitaire;
using Moq;
using Microsoft.AspNetCore.Http;

namespace Projet_Test
{
    public class TestControllerPee
    {
        private AFPANADbContext _dbContext = null;
        private IConfiguration _configuration = null;
        private IHostEnvironment _hostEnvironment = null;
        private IMailSenderMock.IMailSenderMock _mailSenderMock = null;

        [SetUp]
        public void Setup()
        {
            _dbContext = new DbContextMocker().GetAFPANADbContext("AFPANA");
            Dictionary<string, string> keys = new();
            _configuration = IConfigurationMock.IConfigurationMock.GetIConfiguration(keys);
            _hostEnvironment = IHostEnvironmentMock.GetHostEnvironment();
        }

        [Test]
        public void ListePeeAValiderIsValide()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock) {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("1603870");
            var result = controller.ListePeeAValider("1603870", null).Result;
            Assert.IsInstanceOf<ViewResult>(result);
            
        }

        [Test]
        public void ListePeeAValiderIsValidePagination()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("1603870");
            var result = controller.ListePeeAValider("1603870", null).Result as ViewResult;

            Assert.IsInstanceOf<PagingList<ListePeeAValiderModelView>>(result.Model);
        }

        [Test]
        public void ListePeeAValiderIdNoFound()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = controller.ListePeeAValider(string.Empty, null).Result as NotFoundResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void PeeEntrepriseValidationEntreprise()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("4");
            var result = controller.PeeEntrepriseValidation(4).Result;

            Assert.IsInstanceOf<ActionResult>(result);
            Assert.Pass();
        }

        [Test]
        public void PeeEntrepriseValidationIdNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = controller.PeeEntrepriseValidation(null).Result as NotFoundResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void PeeEntrepriseValidationEntrepriseNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("-1");
            var result = controller.PeeEntrepriseValidation(-1).Result as BadRequestResult;

            Assert.That((HttpStatusCode)result.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void EnregistrementPeeInfoIsValid()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("4");
            var result = controller.EnregistrementPeeInfo(4).Result;

            Assert.IsInstanceOf<PartialViewResult>(result);
        }

        [Test]
        public void EnregistrementPeeInfoIsIdNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = controller.EnregistrementPeeInfo(null).Result;

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void EnregistrementPeeInfoPeeIsNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            //contextMock.Request.Setup(r => r.Query["id"]).Returns("-1");
            var result = controller.EnregistrementPeeInfo(-1).Result;

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public void ValidationObjectPeeUpdate()
        {
            PeeModelView peeModelView = new PeeModelView()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto"
            };
            Assert.IsFalse(ValidationService.ValidateModel(peeModelView).Any
                (va => va.MemberNames.Contains("Remarque")
                && va.ErrorMessage.Contains("Les remarques ne doivent pas excéder 1024 caractères.")));
        }

        [Test]
        public void ValidationObjectPeeUpdateNoValid()
        {
            PeeModelView peeModelView = new PeeModelView()
            {
                IdPee = 4,
                MatriculeBeneficiaire = "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
                "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632"
            };
            Assert.IsTrue(ValidationService.ValidateModel(peeModelView).Any
                (va => va.MemberNames.Contains("Remarque")
                && va.ErrorMessage.Contains("Les remarques ne doivent pas excéder 1024 caractères.")));
        }

        [Test]
        public void EnregistrementPeeInfoIsValueOK()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };
            PeeModelView peeModelView = new PeeModelView() 
            { 
                IdPee = 4,
                MatriculeBeneficiaire= "20022801",
                IdTuteur = 1,
                IdResponsableJuridique = 1,
                IdEntreprise = 2,
                IdOffreFormation = 20101,
                IdEtablissement = "19011",
                EtatPee = 0,
                Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
                ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto"
            };

            var result = controller.EnregistrementPeeInfo(4,peeModelView).Result as RedirectToActionResult;
            Assert.That(result.ControllerName, Is.EqualTo("PeeController"));
            Assert.That(result.ActionName, Is.EqualTo("PrevenirBeneficaire"));

        }

        [Test]
        public void EnregistrementPeeInfo_Enregistrement_NonValide_Redirection()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };
            //PeeModelView peeModelView = new PeeModelView()
            //{
            //    IdPee = 4,
            //    MatriculeBeneficiaire = "20022801",
            //    IdTuteur = 1,
            //    IdResponsableJuridique = 1,
            //    IdEntreprise = 2,
            //    IdOffreFormation = 20101,
            //    IdEtablissement = "19011",
            //    EtatPee = 0,
            //    Remarque = "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
            //    "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
            //    "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
            //    "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
            //    "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632" +
            //    "Besoin de plus de renseignements au niveau pédagogique.Et quelques informations de plus à voir" +
            //    ".Recontacter l'entreprise pour avoir des informations complémentaires.Merci.Toto 123456+789912345698745632"
            //};
            //contextMock.Request.Setup(r => r.Query["IdPee"]).Returns("1603870");

            Mock<PeeModelView> parameter = new Mock<PeeModelView>();
            parameter.Setup(i=>i.IdPee).Returns(4);

            var result = controller.EnregistrementPeeInfo(4, parameter.Object).Result as RedirectToActionResult;
            
            Assert.That(result.ControllerName, Is.EqualTo("PeeController"));
            Assert.That(result.ActionName, Is.EqualTo("ListePeeAValider"));
        }
    }
}
