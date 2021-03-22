
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Layers;

namespace AppAfpaBrive.Web.Controllers.Formateur
{
    public class StagiaireParOffredeFormationController : Controller
    {
        private BeneficiaireLayer _beneficiaireLayer = null;
      

        public StagiaireParOffredeFormationController (AFPANADbContext context)
        {
          _beneficiaireLayer  = new BeneficiaireLayer(context)  ;
            
        } 
        // GET: StagiaireParOffredeFormationController
        public ActionResult ListeStagiaireParOffreFormation()
        {
            this.ViewBag.MonTitre = "Liste Stagiaire Par OffreDeFormation";
          var query =_beneficiaireLayer.GetAllByOffredeFormation();
            
            return View(query);
        }
        

        // GET: StagiaireParOffredeFormationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StagiaireParOffredeFormationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StagiaireParOffredeFormationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: StagiaireParOffredeFormationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StagiaireParOffredeFormationController/Edit/5
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

        // GET: StagiaireParOffredeFormationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StagiaireParOffredeFormationController/Delete/5
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
