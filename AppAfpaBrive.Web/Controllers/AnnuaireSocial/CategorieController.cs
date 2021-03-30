using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.AnnuaireSocialLayer;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.AnnuaireSocial
{
    public class CategorieController : Controller
    {

        private readonly AFPANADbContext _context;
        private readonly CategorieLayer _categorieLayer;

        public CategorieController(AFPANADbContext context) 
        {
            _context = context;
            _categorieLayer = new CategorieLayer(_context);
        }


        // GET: CategorieController
        public async Task<IActionResult> Index(string filter, int page, string sortExpression = "LibelleCategorie")

        {
            var model = await _categorieLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // GET: CategorieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategorieModelView cat)
        {
            if (ModelState.IsValid)
            {
                _categorieLayer.Insert(cat.GetCategorie());
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            CategorieModelView obj = _categorieLayer.GetCategorieById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategorieModelView cat)
        {
            if (ModelState.IsValid)
            {
                _categorieLayer.Update(cat.GetCategorie());
                return RedirectToAction("Index");
            }
            return View(cat);
        }


        // POST: CategorieController/Delete/5
        
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categorie structure = _categorieLayer.GetCategorie(id);

            if (structure == null)
            {
                return NotFound();
            }

            _categorieLayer.Delete(structure);
            return RedirectToAction(nameof(Index));
        }
    }
}
