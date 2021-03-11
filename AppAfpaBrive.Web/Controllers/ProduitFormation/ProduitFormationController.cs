using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;


namespace AppAfpaBrive.Web.Controllers.ProduitFormation
{
    public class ProduitFormationController : Controller
    {
        private readonly AFPANADbContext _db;

        public ProduitFormationController(AFPANADbContext db)
        {
            _db = db;
        }

        // GET: ProduitFormation
        public IActionResult Index()
        {
            IEnumerable<BOL.ProduitFormation> listProduitFormations = _db.ProduitFormations.OrderBy(x => x.LibelleProduitFormation);
            for(int i=0; i < listProduitFormations.Count(); i++)
            {
              listProduitFormations = _db.ProduitFormations.Take(20);
            }
            return View(listProduitFormations);
        }

        // GET: ProduitFormation/Details/5
        public IActionResult Details(string Libelle)
        {

            return View();
        }

        // GET: ProduitFormation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProduitFormation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BOL.ProduitFormation obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProduitFormations.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.View(obj);
            
        }

        // GET: ProduitFormation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProduitFormation/Edit/5
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

        // GET: ProduitFormation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProduitFormation/Delete/5
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
