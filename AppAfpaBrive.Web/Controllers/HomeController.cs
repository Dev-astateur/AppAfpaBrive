using AppAfpaBrive.DAL;
using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Data;

namespace AppAfpaBrive.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _mailSender;
        private readonly AFPANADbContext _dbContext;
      
        public HomeController(ILogger<HomeController> logger, 
            IEmailSender mailSender,AFPANADbContext context)
        {
            _logger = logger;
            _mailSender = mailSender;
            _dbContext = context;
        }
        public IActionResult Mail()
        {
            _mailSender.SendEmailAsync("vincent.bost@afpa.fr", "Test", "test");
            return View();
        }
        public IActionResult ListeCollaborateurs1()
        {
            List<Etablissement> etablissements = _dbContext.Etablissements.ToList();
            return View(etablissements);
        }
        public IActionResult ListeCollaborateurs2(string etablissement)
        {
            var collaborateurs = _dbContext.CollaborateurAfpas.Where(c => c.IdEtablissement == etablissement);
            return PartialView("_ListeCollaborateurs", collaborateurs);
           
        }
        public IActionResult ListeRome()
        {
            var of = _dbContext.OffreFormations.Where(of => of.IdEtablissement == "19011" && of.IdOffreFormation == 20102);
            var pf = of.Select(o => o.CodeProduitFormationNavigation);
            var ro = pf.Select(p => p.ProduitFormationAppellationRomes.Select(r=>r.CodeRome)).ToList();
            
            //List<String> romes = _dbContext.OffreFormations.Where(of => of.IdEtablissement == "19011" && of.IdOffreFormation == 9952)
            //    .Select(of => of.CodeProduitFormationNavigation.ProduitFormationAppellationRomes.Select(pfa => pfa.CodeRome)).ToList();
            return View();
        }
        public IActionResult Index()
        {
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
