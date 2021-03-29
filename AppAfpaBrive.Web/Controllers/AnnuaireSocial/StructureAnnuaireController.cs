using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class StructureAnnuaireController : Controller
    {
        // GET: StructureAnnuaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StructureAnnuaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StructureAnnuaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StructureAnnuaireController/Create
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

        // GET: StructureAnnuaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StructureAnnuaireController/Edit/5
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

        // GET: StructureAnnuaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StructureAnnuaireController/Delete/5
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
