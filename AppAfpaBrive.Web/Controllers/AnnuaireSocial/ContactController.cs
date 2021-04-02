using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.AnnuaireSocialLayer;
using AppAfpaBrive.Web.ModelView.AnnuaireModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Threading.Tasks;
using AppAfpaBrive.BOL.AnnuaireSocial;
using System.Diagnostics;

namespace AppAfpaBrive.Web.Controllers.AnnuaireSocial
{
    public class ContactController : Controller
    {

        private readonly AFPANADbContext _context;
        private readonly ContactLayer _contactLayer;

        public ContactController(AFPANADbContext context)
        {
            _context = context;
            _contactLayer = new ContactLayer(_context);
        }

        // GET: ContactController
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactModelView contact)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine("Titre civilité : " + contact.IdTitreCivilite);
                _contactLayer.Insert(contact.GetContact());
                return RedirectToAction("Index");

            }
            return View(contact);
        }

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactController/Edit/5
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

        // GET: ContactController/Delete/5
        
    

        
    }
}
