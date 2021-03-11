using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur
{
    public class StagiaireParOffredeFormationController : Controller
    {
        private readonly AFPANADbContext _AFPANADbContext = null;

        public StagiaireParOffredeFormationController (AFPANADbContext context)
        {
            this._AFPANADbContext = context;
        }
        // GET: StagiaireParOffredeFormationController
        public ActionResult ListeStagiaireParOffreFormation()
        {
            this.ViewBag.MonTitre = "StagiaireParOffreDeFormation";
            return View();
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
