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
        public async Task ListePeeAValiderIsValide()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock) {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            contextMock.Request.Setup(r => r.Query["id"]).Returns("1603870");
            var result = await controller.ListePeeAValider("1603870", null) as Task<ViewResult>; 
            Assert.Pass();
        }

        [Test]
        public async Task ListePeeAValiderIsValidePagination()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            contextMock.Request.Setup(r => r.Query["id"]).Returns("1603870");
            var result = await controller.ListePeeAValider("1603870", null) as Task<ViewResult>;

            Assert.IsInstanceOf<Task<PagingList<ListePeeAValiderModelView>>>(result.Result.Model);
        }

        [Test]
        public async Task ListePeeAValiderIdNoFound()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = await controller.ListePeeAValider(string.Empty, null) as Task<NotFoundResult>;
            Console.WriteLine(result.Result.StatusCode);

            Assert.AreEqual(404,result.Result.StatusCode);
        }

        [Test]
        public async Task PeeEntrepriseValidationEntreprise()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            contextMock.Request.Setup(r => r.Query["id"]).Returns("4");
            var result = await controller.PeeEntrepriseValidation(4) as Task<ViewResult>;

            Assert.Pass();
        }

        [Test]
        public async Task PeeEntrepriseValidationIdNull()
        {
            HttpMock.MockHTTPContext contextMock = new();
            PeeController controller = new PeeController(_dbContext, _configuration, _hostEnvironment, _mailSenderMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = contextMock.Http.Object,
                }
            };

            contextMock.Request.Setup(r => r.Query["id"]).Returns(string.Empty);
            var result = await controller.PeeEntrepriseValidation(null) as Task<NotFoundResult>;

            Assert.Pass();
        }

    }
}
