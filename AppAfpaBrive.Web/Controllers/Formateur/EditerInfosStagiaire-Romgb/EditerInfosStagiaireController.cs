using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{
    public class EditerInfosStagiaireController : Controller
    {
        private AFPANADbContext _context = null;
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

        //GET: EditerInfosStagiaireController
        public ActionResult ListeOffreFormation()
        {
            this.ViewBag.MonTitre = "ListeOffreFormation";
            var query = _offreFormation.GetAllOffreFormation();

            List<SelectListItem> listeOffreFormation = new List<SelectListItem>();

            foreach (var item in query)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Value = item.IdOffreFormation.ToString();
                selectListItem.Text = item.LibelleOffreFormation;
                listeOffreFormation.Add(selectListItem);
                ViewBag.DropDownListItems = listeOffreFormation;
            }
            return View(query);
        }

        //public ActionResult Create()
        //{
        //    this.ViewBag.MonTitre = "Create";
        //    var query = _offreFormation.GetAllOffreFormation().ToList();
        //    return View(query);
        //}

        //// GET: EditerInfosStagiaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
