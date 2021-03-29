using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class GestionAnnuaireController : Controller
    {
        // GET: GerstionAnnuaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GerstionAnnuaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GerstionAnnuaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GerstionAnnuaireController/Create
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

        // GET: GerstionAnnuaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GerstionAnnuaireController/Edit/5
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

        // GET: GerstionAnnuaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GerstionAnnuaireController/Delete/5
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
