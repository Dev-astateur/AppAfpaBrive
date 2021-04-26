using AppAfpaBrive.BOL.AnnuaireSocial;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.AnnuaireSocial;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    [Authorize(Roles = "CollaborateurAFPA,Administrateur")]
    public class GestionAnnuaireController : Controller
    {
        private readonly CategorieLayer _categorieLayer;
        private readonly StructureLayer _structureLayer;
        private readonly ContactLayer _contactLayer;
        private readonly LigneAnnuaireLayer _ligneAnnuaireLayer;
        private readonly ContactLigneAnnuaireLayer _contactLigneAnnuaireLayer;
        private readonly AFPANADbContext _context;

        public GestionAnnuaireController(AFPANADbContext context)
        {
            _context = context;
            _categorieLayer = new CategorieLayer(context);
            _structureLayer = new StructureLayer(context);
            _contactLayer = new ContactLayer(context);
            _ligneAnnuaireLayer = new LigneAnnuaireLayer(context);
            _contactLigneAnnuaireLayer = new ContactLigneAnnuaireLayer(context);
        }



        // GET: GestionAnnuaireController
        public async Task<IActionResult> Index(string filter, int page, string sortExpression = "PublicConcerne")
        {
            var model = await _ligneAnnuaireLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }


        #region Actions categorie


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

        #region Actions structure

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

        #region Actions LigneAnnuaire

        public async Task<IActionResult> LigneAnnuaires(string filter, int page, string sortExpression = "PublicConcerne")
        {
            var model = await _ligneAnnuaireLayer.GetPage(filter, page, sortExpression);
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }


        public IActionResult CreateLigneAnnuaires()
        {

            LigneAnnuaireEtape1ModelView ligne = new()
            {
                listCategories = _categorieLayer.categories()
            };

            return View(ligne);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLigneAnnuaires(LigneAnnuaireEtape1ModelView ligneAnnuaire)
        {

            ligneAnnuaire.categories = new List<Categorie>();

            if (ModelState.IsValid)
            {
                foreach (CategorieModelView cb in ligneAnnuaire.listCategories)
                {
                    if (cb.IsChecked)
                    {

                        ligneAnnuaire.categories.Add(cb.GetCategorie());
                    }
                }


                string str = JsonConvert.SerializeObject(ligneAnnuaire);
                HttpContext.Session.SetString("ligneAnnuaire", str);


                return RedirectToAction("CreateLigneAnnuaireAddContact");
            }


            ligneAnnuaire.listCategories = _categorieLayer.categories();
            return View(ligneAnnuaire);
        }



        public IActionResult CreateLigneAnnuaireAddContact()
        {
            var model = _contactLayer.Contacts();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLigneAnnuaireAddContact(List<ContactModelView> modelViews)
        {

            string str = this.HttpContext.Session.GetString("ligneAnnuaire");
            LigneAnnuaireEtape1ModelView ligne = JsonConvert.DeserializeObject<LigneAnnuaireEtape1ModelView>(str);
            ligne.structure = _structureLayer.GetStructureById(ligne.IdStructure).GetStructure();

            foreach (var el in modelViews)
            {
                if (el.IsChecked)
                {
                    ligne.contacts.Add(el.GetContact());
                }
            }

            LigneAnnuaire final = ligne.ToLigneAnnuaire();
            _ligneAnnuaireLayer.Insert(final);

            return RedirectToAction("LigneAnnuaires");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if ( id is null )
            {
                return BadRequest();
            }

            LigneAnnuaireEtape1ModelView annuaire = await _ligneAnnuaireLayer.GetLigneAnnuaireByIdAsync((int)id);
            annuaire.listStructures = _structureLayer.GetListStructure(annuaire.IdStructure);
            return View(annuaire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, LigneAnnuaireEtape1ModelView ligneAnnuaire)
        {
            if (id != ligneAnnuaire.IdLigneAnnuaire)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                string annuaire = JsonConvert.SerializeObject(ligneAnnuaire);
                HttpContext.Session.SetString("ligneAnnuaire", annuaire);

                return RedirectToAction("UpdateContactLigneAnnuaire");
            }
            else
            {
                return View(ligneAnnuaire);
            }               
        }

        public IActionResult UpdateContactLigneAnnuaire ()
        {
            string str = this.HttpContext.Session.GetString("ligneAnnuaire");
            LigneAnnuaireEtape1ModelView ligneAnnuaire = JsonConvert.DeserializeObject<LigneAnnuaireEtape1ModelView>(str);
            if (ligneAnnuaire is null)
            {
                return BadRequest();
            }
            ICollection<ContactModelView> listeContacts = _contactLigneAnnuaireLayer.GetContactsByIdLigneAnnuaire(ligneAnnuaire.IdLigneAnnuaire);
            var contacts = _contactLayer.GetContactsChecksAll();
            foreach(ContactModelView item in contacts)
            {
                if ( !listeContacts.Contains(item) )
                    listeContacts.Add(item);
            }

            return View(listeContacts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateContactLigneAnnuaire(ICollection<ContactModelView> modelViews)
        {

            string str = this.HttpContext.Session.GetString("ligneAnnuaire");
            LigneAnnuaireEtape1ModelView ligneAnnuaire = JsonConvert.DeserializeObject<LigneAnnuaireEtape1ModelView>(str);
            if (ligneAnnuaire is null)
                return BadRequest();

            ligneAnnuaire.structure = _structureLayer.GetStructureById(ligneAnnuaire.IdStructure).GetStructure();
            var contacts = _contactLigneAnnuaireLayer.GetContactsByIdLigneAnnuaire(ligneAnnuaire.IdLigneAnnuaire);

            foreach (ContactModelView item in modelViews)
            {
                if (item.IsChecked)
                {
                    if(!contacts.Contains(item))
                    {
                        ligneAnnuaire.contacts.Add(item.GetContact());
                    }
                    //ligne.contacts.Add(el.GetContact());
                }
            }

            LigneAnnuaire final = ligneAnnuaire.ToLigneAnnuaire();
            _ligneAnnuaireLayer.Update(final);

            return RedirectToAction("LigneAnnuaires");
        }

        #endregion
    }
}
