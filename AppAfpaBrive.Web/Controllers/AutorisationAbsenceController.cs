using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlHelpers.Word;

namespace AppAfpaBrive.Web.Controllers
{
    public class AutorisationAbsenceController : Controller
    {
        
        private readonly AFPANADbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        public AutorisationAbsenceController(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
            _dbContext = context;
            _config = config;
            _env = env;
           
        }

        public IActionResult ChargerDocAutorisation()
        {
           //recuperer modele
            string path = Path.Combine(_env.ContentRootPath, "ModelesOffice\\AutorisationAbsence.docx");
            // destination
            string destinationPath = Path.Combine(_env.ContentRootPath, @$"\AutorisationAbsence\Autorisation_{DateTime.Now-DateTime.MinValue}");
            //Copie du fifchier avec possibilite d'écrire"
            System.IO.File.Copy(path, destinationPath, true);
            //Remplacer les MergeFields
            using (WordprocessingDocument document=WordprocessingDocument.Open(destinationPath, true))
            {
                var mergeFields = document.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Nom").ReplaceWithText("SIRE");
                mergeFields.WhereNameIs("prenom").ReplaceWithText("Romain");
                document.MainDocumentPart.Document.Save();
            }
            return View();

            

            //Ouvrir le package de documents Word
            //WordprocessingDocument document = WordprocessingDocument.Open("C:\\Users\\CDA\\Desktop\\Projet_Mars2021\\AppAfpaBrive.Web\\ModelesOffice\\AutorisationAbsence.docx", true);

            //Acceder au corps de la partie principale du document
           // Body body = document.MainDocumentPart.Document.Body;
            return View();
        }
        // GET: AutorisationAbsence
        public ActionResult Index()
        {
            return View();
        }

        // GET: AutorisationAbsence/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AutorisationAbsence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutorisationAbsence/Create
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

        // GET: AutorisationAbsence/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AutorisationAbsence/Edit/5
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

        // GET: AutorisationAbsence/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AutorisationAbsence/Delete/5
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
