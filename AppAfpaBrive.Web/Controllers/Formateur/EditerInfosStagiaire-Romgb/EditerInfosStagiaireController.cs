using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{
    public class EditerInfosStagiaireController : Controller
    {
        //private AFPANADbContext _context = null;
        private readonly StagiaireLayer _stagiaireLayer;
        private readonly OffreFormationLayer _offreFormation;


        public EditerInfosStagiaireController(AFPANADbContext context)
        {
            _offreFormation = new OffreFormationLayer(context);
            _stagiaireLayer = new StagiaireLayer(context);
        }

        //public StagiaireLayer(AFPANADbContext context)
        //{
        //    _stagiaireLayer = new StagiaireLayer(context);
        //}
        //public EditerInfosStagiaireController(AFPANADbContext context)
        //{
        //    this _context = context;
        //}

        //Remplir DropDown
        private void RemplirListOffreFormation()
        {

        }

        //GET: EditerInfosStagiaireController
        public async Task<ActionResult> ListeOffreFormation(string tbRechercherOFormation)
        {
            this.ViewBag.MonTitre = "ListeOffreFormation";
            //var query = _offreFormation.GetAllOffreFormation();
            //var query3 = _offreFormation.GetOffreFormationStartsWith(tbRechercherOFormation);
            var query2 = _offreFormation.GetOffreFormationByContains(tbRechercherOFormation);
            //var query3 = _stagiaireLayer.GetAllStagiaires();

            return View(query2);
        }

        //public ActionResult Create()
        //{
        //    this.ViewBag.MonTitre = "Create";
        //    var query = _offreFormation.GetAllOffreFormation().ToList();
        //    return View(query);
        //}

        //// GET: EditerInfosStagiaireController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _stagiaireLayer.GetBeneficiaireParOffreDeFormation();
            return View(obj);
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
            string matricule = HttpContext.Request.Query["term"].ToString();


            if (ModelState.IsValid)
            {
                //on enregistre en bdd
            }
             // Sinon on ne fait rien
            
            try
            {
                
                  //  return View();
                
                return RedirectToAction("Edit", "EditerInfosStagiaire2");

            }
            catch
            {
                return View();
            }
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
