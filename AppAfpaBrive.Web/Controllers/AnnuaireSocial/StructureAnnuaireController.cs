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

namespace AppAfpaBrive.Web.Controllers
{
    public class StructureAnnuaireController : Controller
    {

        private readonly AFPANADbContext _context;
        private readonly StructureLayer _structureLayer;

        public StructureAnnuaireController(AFPANADbContext context)
        {
            _context = context;
            _structureLayer = new StructureLayer(context);
        }

        // GET: StructureAnnuaireController
        public async Task<IActionResult> Index(string filter, int page, string sortExpression = "NomStructure")
        {
            var model = await _structureLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
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
        public ActionResult Create(StructureModelView structure)
        {
            if (ModelState.IsValid)
            {
                _structureLayer.Insert(structure.GetStructure());
                return RedirectToAction("Index");

            }
            return View(structure);
        }

        // GET: StructureAnnuaireController/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            StructureModelView obj = _structureLayer.GetStructureById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: StructureAnnuaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StructureModelView obj)
        {

            if (ModelState.IsValid)
            {
                _structureLayer.Update(obj.GetStructure());
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        // POST: StructureAnnuaireController/Delete/5


        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Structure structure = _structureLayer.GetStructure(id);

            if (structure == null)
            {
                return NotFound();
            }

            _structureLayer.Delete(structure);
            return RedirectToAction(nameof(Index));
        }
    }
}
