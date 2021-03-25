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

using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Routing;
using AppAfpaBrive.Web.Layers;

namespace AppAfpaBrive.Web.Controllers.ProduitFormation
{
    public class ProduitFormationController : Controller
    {
        private readonly AFPANADbContext _db;
        //private readonly ProduitDeFormationLayer _produitDeFormationLayer;

        
        public ProduitFormationController(AFPANADbContext db)
        {
            _db = db;
        }



        // GET: ProduitFormation
        public async Task<IActionResult> Index(string filter,int pageIndex, string sortExpression="CodeProduitFormation")
        {
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            var model = await _produitDeFormationLayer.GetPage(filter, pageIndex, sortExpression);
            model.Action = "Index";
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
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
        public IActionResult Create(ProduitFormationModelView obj)
        {
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            //var x = Request.Form["Formation"].ToString();
            
            if (ModelState.IsValid)
            {
                //if (x == "0")
                //{
                //    obj.FormationContinue = true;
                //}
                //else obj.FormationDiplomante = true;
                _produitDeFormationLayer.InsertProduit(obj.GetProduitFormation());
                return RedirectToAction("Index");
            }
            return this.View(obj);
            
        }

        // GET: ProduitFormation/Edit/5
        public IActionResult Edit(int id)
        {
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            if (id == 0)
            {
                return NotFound();
            }
            ProduitFormationModelView obj = _produitDeFormationLayer.GetByCodeProduitFormation(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: ProduitFormation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProduitFormationModelView obj)
        {
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            //var x = Request.Form["Formation"].ToString();
            if (ModelState.IsValid)
            {
                //if (x == "0")
                //{
                //    obj.FormationContinue = true;
                //}
                //else obj.FormationDiplomante = true;
                _produitDeFormationLayer.Update(obj.GetProduitFormation());
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: ProduitFormation/Delete/5
        public IActionResult Delete(int? id)
        {
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _produitDeFormationLayer.GetByCodeProduitFormationdelete((int)id);
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
            ProduitDeFormationLayer _produitDeFormationLayer = new ProduitDeFormationLayer(_db);
            ProduitFormationModelView obj = _produitDeFormationLayer.GetByCodeProduitFormation(id);
            if (obj == null)
            {
                return NotFound();
            }
            _produitDeFormationLayer.Remove(obj.GetProduitFormation());
            return RedirectToAction("Index");
        }
    }
}
