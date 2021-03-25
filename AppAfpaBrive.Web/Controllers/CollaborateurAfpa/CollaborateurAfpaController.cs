using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.CollaborateurAfpa
{
    public class CollaborateurAfpaController : Controller
    {
        private readonly AFPANADbContext _db;

        public CollaborateurAfpaController(AFPANADbContext db)
        {
            _db = db;
        }
        // GET: CollaborateurAfpaController
        public async Task<IActionResult> Index(string filter, int pageIndex, string sortExpression = "NomCollaborateur")
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);

            var model = await _collaborateurLayer.GetPage(filter, pageIndex, sortExpression);
            model.Action = "Index";
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // GET: CollaborateurAfpaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollaborateurAfpaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollaborateurAfpaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CollaborateurAfpaModelView obj)
        {
            var x = Request.Form["TitreCivilite"].ToString();
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (ModelState.IsValid)
            {
                if (x == "0")
                {
                    obj.CodeTitreCivilite = 0;
                }
                else obj.CodeTitreCivilite = 1;
                _collaborateurLayer.InsertProduit(obj.GetCollaborateur());
                return RedirectToAction("Index");
            }
            return this.View(obj);
        }

        // GET: CollaborateurAfpaController/Edit/5
        public IActionResult Edit(string id)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (id == null)
            {
                return NotFound();
            }
            CollaborateurAfpaModelView obj = _collaborateurLayer.GetByMatriculeCollaborateur(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: CollaborateurAfpaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CollaborateurAfpaModelView obj)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (ModelState.IsValid)
            {
                _collaborateurLayer.Update(obj.GetCollaborateur());
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: CollaborateurAfpaController/Delete/5
        public IActionResult Delete(string id)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (id == null)
            {
                return NotFound();
            }
            var obj = _collaborateurLayer.GetByMatriculeCollaborateurDelete(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: CollaborateurAfpaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCollaborateur(string id)
        {
            
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            CollaborateurAfpaModelView obj = _collaborateurLayer.GetByMatriculeCollaborateur(id);
            if (obj == null)
            {
                return NotFound();  
            }
            _collaborateurLayer.Remove(obj.GetCollaborateur());
            return RedirectToAction("Index");
        }
    }
}
