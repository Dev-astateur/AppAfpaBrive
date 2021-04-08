using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ViewModels.IntegrationExcelOffre;
using Microsoft.Extensions.Configuration;
namespace AppAfpaBrive.Web.Controllers
{
    public class IntegrationExcelOffresController : Controller
    {
        private readonly AFPANADbContext _context;
        private readonly IConfiguration _config;
       
        public IntegrationExcelOffresController(AFPANADbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View(new ViewModels.IntegrationExcelOffre.IntegrationExcelOffreCreate());
        }
        [HttpPost]
   
        public IActionResult Create([Bind("CodeProduitFormation,MatriculeCollaborateurAfpa,PathFileIntegration")] IntegrationExcelOffreCreate integrationExcelOffreCreate)
        {
            Utilitaires.IntegrationExcelOffre integration = new Utilitaires.IntegrationExcelOffre(_config,_context);
    
            integration.IntegrerDonnees(integrationExcelOffreCreate.MatriculeCollaborateurAfpa, integrationExcelOffreCreate.CodeProduitFormation,
                @integrationExcelOffreCreate.PathFileIntegration);

                return RedirectToAction(nameof(Create));
           
        }
    }
}
