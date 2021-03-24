using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{


    public class EditerInfosStagiaireController : Controller
    {
        private AFPANADbContext _context = null;
        private readonly StagiaireLayer _stagiaireLayer;
        private readonly OffreFormationLayer _offreFormation;
        private readonly BeneficiaireLayer _beneficiaireLayer;


        public EditerInfosStagiaireController(AFPANADbContext context)
        {
            _offreFormation = new OffreFormationLayer(context);
            _stagiaireLayer = new StagiaireLayer(context);
        }

        //GET: EditerInfosStagiaireController
        public async Task<ActionResult> ListeOffreFormation(string tbRechercherOFormation)
        {
            this.ViewBag.MonTitre = "ListeOffreFormation";
            var query2 = _offreFormation.GetOffreFormationByContains(tbRechercherOFormation);

            return View(query2);
        }

        //Composant de pagination
        //public async Task<IActionResult> ChargerListeStagiaires(int page = 1)
        //{
        //    var beneficiaires =_context.Beneficiaires.OrderBy(x => x.NomBeneficiaire);
        //    var model = await PagingList.CreateAsync(beneficiaires, 5, page);
        //    return PartialView("_VuePartielleStagiaires", model);
        //}


        //public IActionResult ChargerListeStagiaires(int idOffreFormation)
        //{
        //    var beneficiaires = _stagiaireLayer.GetBeneficiaireParIdOffreDeFormation(idOffreFormation);
        //    return PartialView("_VuePartielleStagiaires", beneficiaires);
        //}

        
        public async Task<IActionResult> ChargerListeStagiaires(string libelle)
        {
            var beneficiaires = _stagiaireLayer.GetBeneficiaireParLibelleOffreDeFormation(libelle);
            return PartialView("_VuePartielleStagiaires", beneficiaires);
        }

        // GET: EditerInfosStagiaireController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: EditerInfosStagiaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EditerInfosStagiaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return View();
        }

        // GET: EditerInfosStagiaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EditerInfosStagiaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaireController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
