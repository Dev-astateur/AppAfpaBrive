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
using AppAfpaBrive.DAL.Layers;

namespace AppAfpaBrive.Web.Controllers.ProduitFormation
{
    public class ProduitFormationController : Controller
    {
        //private readonly AFPANADbContext _db;
        private readonly ProduitDeFormationLayer _produitDeFormationLayer;

        
        public ProduitFormationController(AFPANADbContext db)
        {
            _produitDeFormationLayer = new ProduitDeFormationLayer(db);
            
        }

        // GET: ProduitFormation
        public IActionResult Index(int id=0 )
        {

            IEnumerable<BOL.ProduitFormation> listProduitFormations;
           
            //if (id != 0)
            //{
            //    //for (int i = 0; i < listProduitFormations.Count(); i++)
            //    //{
            //    listProduitFormations = _db.ProduitFormations.Skip(20 * (id - 1)).Take(20);
            //    //}
            //    return View(listProduitFormations);
            //}
            listProduitFormations = _produitDeFormationLayer.GetAll();
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
                _produitDeFormationLayer.InsertProduit(obj);
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
            var obj = _produitDeFormationLayer.GetByCodeProduitFormation(id);
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
                _produitDeFormationLayer.Update(obj);
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
            var obj = _produitDeFormationLayer.GetByCodeProduitFormation((int)id);
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
            var obj = _produitDeFormationLayer.GetByCodeProduitFormation(id);
            if(obj == null)
            {
                return NotFound();
            }
            _produitDeFormationLayer.Remove(obj);
            return RedirectToAction("Index");
        }
    }
}
