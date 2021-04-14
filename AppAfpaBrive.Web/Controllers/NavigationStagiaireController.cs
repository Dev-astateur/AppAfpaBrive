using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Authorization;

namespace AppAfpaBrive.Web.Controllers
{
    [Authorize(Roles = "Bénéficiaire")]
    public class NavigationStagiaireController : Controller
    {
        private readonly Layer_Beneficiaire _layerBeneficaire = null;

        public NavigationStagiaireController(AFPANADbContext dbContext)
        {
            _layerBeneficaire = new Layer_Beneficiaire(dbContext);
        }

        /// <summary>
        /// action qui affiche la navigation du stagiaire
        /// </summary>
        /// <param name="id">matricule du stagiaire</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string id = HttpContext.User.Identity.Name;
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            BeneficiaireNavigationModelView model = await _layerBeneficaire.BeneficiaireByIdAsync(id);
            return View(model);
        }
    }
}
