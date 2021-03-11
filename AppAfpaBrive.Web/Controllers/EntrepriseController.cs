using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly AFPANADbContext _dbContext;

        public EntrepriseController (AFPANADbContext Db)
        {
            _dbContext = Db;
        }

        // GET: EntrepriseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EntrepriseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntrepriseController/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: EntrepriseController/ListeEntreprise
        public ActionResult ListeEntreprise()
        {
           
            return View();
        }
        // GET: EntrepriseController/creerEntreprise
        [HttpGet]
        public ActionResult CreerEntreprise()
        
        {
            return View();
        }
        // POST: EntrepriseController/creerEntreprise
        [HttpPost]
        public ActionResult CreerEntreprise(Entreprise entreprise)
        {
            _dbContext.Entreprises.Add(entreprise);
            _dbContext.SaveChanges();
            return View();
        }


        // POST: EntrepriseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EntrepriseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntrepriseController/Edit/5
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

        // GET: EntrepriseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntrepriseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
