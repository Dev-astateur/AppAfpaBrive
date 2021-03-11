using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{
    public class EditerInfosStagiaireController : Controller
    {
        // GET: EditerInfosStagiaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EditerInfosStagiaireController/Details/5
        public ActionResult Details(int id)
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
            
            try
            {
                if (matricule == null || matricule.Length < 8)
                {
                    return View();
                }
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
