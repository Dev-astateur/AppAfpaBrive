using AppAfpaBrive.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using AppAfpaBrive.Web.Logging;

namespace AppAfpaBrive.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _mailSender;
        public HomeController(ILogger<HomeController> logger, IEmailSender mailSender)
        {
            _logger = logger;
            _mailSender = mailSender;
        }
        public IActionResult Mail()
        {
            _mailSender.SendEmailAsync("vincent.bost@afpa.fr", "Test", "test");
            

            return View();
        }
        public IActionResult Index()
        {
            LoggingAppAfpaBrive test = new LoggingAppAfpaBrive();
            test.CreateEventSource();
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
