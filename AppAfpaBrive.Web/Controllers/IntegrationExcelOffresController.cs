using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ViewModels.IntegrationExcelOffre;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using AppAfpaBrive.Web.Areas.Identity.Data;

namespace AppAfpaBrive.Web.Controllers
{
    public class IntegrationExcelOffresController : Controller
    {
        private readonly AFPANADbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        public IntegrationExcelOffresController(AFPANADbContext context,IConfiguration config, UserManager<AppAfpaBriveUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View(new ViewModels.IntegrationExcelOffre.IntegrationExcelOffreCreate());
        }
        [HttpPost]
   
        public IActionResult Create([Bind("CodeProduitFormation,MatriculeCollaborateurAfpa,PathFileIntegration")] IntegrationExcelOffreCreate integrationExcelOffreCreate)
        {
            Utilitaires.IntegrationExcelOffre integration = new Utilitaires.IntegrationExcelOffre(_config,_context,_userManager);
    
            integration.IntegrerDonnees(integrationExcelOffreCreate.MatriculeCollaborateurAfpa, integrationExcelOffreCreate.CodeProduitFormation,
                @integrationExcelOffreCreate.PathFileIntegration);

                return RedirectToAction(nameof(Create));
           
        }
    }
}
