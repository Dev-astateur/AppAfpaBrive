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
using System.Collections.Generic;
using Moq;
using AppAfpaBrive.Web.Controllers.ApiControllers;
using System.ServiceProcess;
using System.Web;
using System.Net.Http;
using Projet_Test.InMemoryDb;
using System.Threading.Tasks;
using System.Web.Http;

namespace Projet_Test
{
    class TestFormateurApiController
    {

        private HttpServer Server;
        private string UrlBase = "";

        [SetUp]
        public void Setup()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "Default", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { id= RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Server = new HttpServer(config);

        }

        [Test]
        public void TestGetFormateurStartWith() 
        {

            var client = new HttpClient(Server);
            var request = CreateRequest("api/Orders/GetOrderStatus?companyCode=001&orderNumber=1234", "application/json", HttpMethod.Get);

            ////arrange
            //var dba = DbContextMocker.GetAFPANADbContext(nameof(TestGetFormateurStartWith));
            //var controller = new FormateurApiController(dba);

            //var response = await controller.GetFormateurStartWith() as ObjectResult;
            //var value = response.Value;

            //dba.Dispose();

          

        }

    }
}
