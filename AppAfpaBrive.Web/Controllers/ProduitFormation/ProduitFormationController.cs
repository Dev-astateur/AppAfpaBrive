using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;



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
        public IActionResult Index(int id=0 )
        {
        
            IEnumerable<BOL.ProduitFormation> listProduitFormations = _db.ProduitFormations.OrderBy(x => x.LibelleProduitFormation);
           
            if (id != 0)
            {
                //for (int i = 0; i < listProduitFormations.Count(); i++)
                //{
                listProduitFormations = _db.ProduitFormations.Skip(20 * (id - 1)).Take(20);
                //}
                return View(listProduitFormations);
            }
            listProduitFormations = _db.ProduitFormations.Take(20);
            return View(listProduitFormations);
        }

        // GET: ProduitFormation/Details/5
        public IActionResult Details(string Libelle)
        {

            return View();
        }

        // GET: ProduitFormation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProduitFormation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BOL.ProduitFormation obj)
        {

            //if ((obj.FormationContinue == false && obj.FormationDiplomante == false) ||
            //(obj.FormationDiplomante == true && obj.FormationContinue == true))
            //{
            //    ModelState.AddModelError("Error", "Vous devez selectionner une seule option");
            //}
            //else
            if (ModelState.IsValid)
            {
                _db.ProduitFormations.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.View(obj);
            
        }

        // GET: ProduitFormation/Edit/5
        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var obj = _db.ProduitFormations.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            return View(obj);
        }

        // POST: ProduitFormation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BOL.ProduitFormation obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProduitFormations.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: ProduitFormation/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ProduitFormations.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: ProduitFormation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _db.ProduitFormations.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.ProduitFormations.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
