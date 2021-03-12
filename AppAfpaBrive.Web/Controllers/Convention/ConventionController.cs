using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layer;
using AppAfpaBrive.BOL;

namespace AppAfpaBrive.Web.Controllers.Convention
{
    public class ConventionController : Controller
    {
        private Layer_Offres_Formation _beneficiaireOffre = null;

        public ConventionController (AFPANADbContext context)
        {
            _beneficiaireOffre = new Layer_Offres_Formation(context);
        }

        public IActionResult Index()
        {
            IEnumerable<BeneficiaireOffreFormation> obj = _beneficiaireOffre.GetFormations("azerty12");
            return View(obj);
        }

        public IActionResult Create(int? id)
        {
            ViewData["Message"] = "Your application description page " + id + " !";

            return View();
        }
    }
}
