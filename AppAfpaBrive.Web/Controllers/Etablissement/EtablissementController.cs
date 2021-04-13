using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;

using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Etablissement
{
    
    [Authorize(Roles = "Administrateur,CollaborateurAFPA")]
    public class EtablissementController : Controller
    {
        
        private readonly Layer_Etablissement _etablissementLayer;

        public EtablissementController(AFPANADbContext db)
        {
            _etablissementLayer = new Layer_Etablissement(db);
        }

        // GET: Etablissement
        public async Task<IActionResult> Index(string filter, int page, string sortExpression = "NomEtablissement")
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db); // -- modification de Aurélien

            var model = await _etablissementLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // POST: Etablissement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EtablissementModelView obj)
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db);
            var check = _etablissementLayer.CheckIdEtablissementExiste(obj.IdEtablissement);
            if (check == false)
            {
                ModelState.AddModelError("IdEtablissement", "L'id d'établissement existe deja");
                return View();
            }
            if (ModelState.IsValid)
            {
                _etablissementLayer.InsertProduit(obj.GetEtablissement());
                return RedirectToAction("Index");
            }
            return this.View(obj);
        }

        // GET: EtablissementController/Edit/5
        public IActionResult Edit(string id)
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db);
            if (id == null)
            {
                return NotFound();
            }
            EtablissementModelView obj = _etablissementLayer.GetByIdEtablissement(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: EtablissementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EtablissementModelView obj)
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db); 
            if (ModelState.IsValid)
            {
                _etablissementLayer.Update(obj.GetEtablissement());
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: EtablissementController/Delete/5
        public IActionResult Delete(string id)
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db);
            if (id == null )
            {
                return NotFound();
            }
            var obj = _etablissementLayer.GetByIdEtablissementDelete(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: EtablissementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //Layer_Etablissement _etablissementLayer = new Layer_Etablissement(_db);
            EtablissementModelView obj = _etablissementLayer.GetByIdEtablissement(id.ToString());
            if (obj == null)
            {
                return NotFound();
                
            }
            _etablissementLayer.Remove(obj.GetEtablissement());
            return RedirectToAction("Index");
        }
    }
}

