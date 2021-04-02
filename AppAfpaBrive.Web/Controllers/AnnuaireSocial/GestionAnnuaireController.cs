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
    public class GestionAnnuaireController : Controller
    {


        private  CategorieLayer _categorieLayer;
        private  StructureLayer _structureLayer;
        private  ContactLayer _contactLayer;

        private readonly AFPANADbContext _context;
     

        public GestionAnnuaireController(AFPANADbContext context)
        {
            _context = context;
            _categorieLayer = new CategorieLayer(_context);
            _structureLayer = new StructureLayer(_context);
            _contactLayer = new ContactLayer(_context);
        }



        // GET: GestionAnnuaireController
        public ActionResult Index()
        {
            return View();
        }


        #region Action categorie
      

        public async Task<IActionResult> Categories(string filter, int page, string sortExpression = "LibelleCategorie")
        {
            var model = await _categorieLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }


        // GET: GestionAnnuaireController/Create
        public ActionResult CreateCategorie()
        {
            return View();
        }

        // POST: GestionAnnuaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategorie(CategorieModelView cat)
        {
            if (ModelState.IsValid)
            {
                _categorieLayer.Insert(cat.GetCategorie());
                return RedirectToAction("Categories");
            }
            return View(cat);
        }


        // GET: GestionAnnuaireController/Edit/5
        public ActionResult EditCategorie(int id)
        {
            if (id == 0)
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

        // POST: GestionAnnuaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
      public ActionResult EditCategorie(CategorieModelView cat)
        {
            if (ModelState.IsValid)
            {
                _categorieLayer.Update(cat.GetCategorie());
                return RedirectToAction(nameof(Categories));
            }
            return View(cat);
        }

        // GET: GestionAnnuaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GestionAnnuaireController/Delete/5
    
        public ActionResult DeleteCategorie(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categorie cat = _categorieLayer.GetCategorie(id);

            if (cat == null)
            {
                return NotFound();
            }

            _categorieLayer.Delete(cat);
            return RedirectToAction(nameof(Categories));
        }

        #endregion

        #region Action structure

        public async Task<IActionResult> Structures(string filter, int page, string sortExpression = "NomStructure")
        {
            var model = await _structureLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }


        // GET: StructureAnnuaireController/Create
        public ActionResult CreateStructure()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStructure(StructureModelView structure)
        {
            if (ModelState.IsValid)
            {
                _structureLayer.Insert(structure.GetStructure());
                return RedirectToAction("Structures");

            }
            return View(structure);
        }

        // GET: StructureAnnuaireController/Edit/5
        public IActionResult EditStructure(int id)
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
        public ActionResult EditStructure(StructureModelView obj)
        {

            if (ModelState.IsValid)
            {
                _structureLayer.Update(obj.GetStructure());
                return RedirectToAction("Structures");
            }
            return View(obj);

        }

        public ActionResult DeleteStructure(int id)
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
            return RedirectToAction(nameof(Structures));
        }

        #endregion

        #region Actions contacts

        public async Task<IActionResult> Contacts(string filter, int page, string sortExpression = "Nom")
        {


            var model = await _contactLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // GET: ContactController/Create
        public ActionResult CreateContact()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact(ContactModelView contact)
        {
            if (ModelState.IsValid)
            {
               
                _contactLayer.Insert(contact.GetContact());
                return RedirectToAction("Contacts");

            }
            return View(contact);
        }


        // GET: StructureAnnuaireController/Edit/5
        public IActionResult EditContact(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ContactModelView obj = _contactLayer.GetContactModelViewById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: StructureAnnuaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(ContactModelView obj)
        {

            if (ModelState.IsValid)
            {
                _contactLayer.Update(obj.GetContact());
                return RedirectToAction("Contacts");
            }
            return View(obj);

        }


        public ActionResult DeleteContact(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Contact contact = _contactLayer.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contactLayer.Delete(contact);
            return RedirectToAction(nameof(Contacts));
        }

        #endregion



    }
}
