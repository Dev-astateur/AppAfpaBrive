using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Formateur.EditerInfosStagiaire_Romgb
{
    public class EditerInfosStagiaire2Controller : Controller
    {
        // GET: EditerInfosStagiaire2Controller
        public ActionResult Index()
        {
            return View();
        }

        // GET: EditerInfosStagiaire2Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EditerInfosStagiaire2Controller/Create
        public ActionResult Create()
        {
                return this.View();     
        }

        // POST: EditerInfosStagiaire2Controller/Create
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

        // GET: EditerInfosStagiaire2Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaire2Controller/Edit/5
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

        // GET: EditerInfosStagiaire2Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EditerInfosStagiaire2Controller/Delete/5
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
