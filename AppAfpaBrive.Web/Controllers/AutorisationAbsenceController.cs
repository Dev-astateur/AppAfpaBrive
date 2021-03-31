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
using System.Net.Mime;

namespace AppAfpaBrive.Web.Controllers
{
    public class AutorisationAbsenceController : Controller
    {
        
        //private readonly AFPANADbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        public AutorisationAbsenceController(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
           // _dbContext = context;
            _config = config;
            _env = env;
           
        }
        [HttpGet]
        public ActionResult CompleterInfoAbsence()
        {
            //Faire un choix parmi les motifs d'absence

            return View();
        }
        [HttpPost]
        public ActionResult CompleterInfoAbsence(string motifAbsence)
        {
            string motif="";
           string motifSpecial= Request.Form["motifSpecial"];
            //Je dois recuperer le motif d'absence choisi dans les radioButtons
            switch (motifAbsence)
            {
                case "JournéeDefense":
                    motif = "Journée défense et citoyenneté / Céremonie d'accueil dans la citoyenneté (1 jour ouvré)";
                    break;
                case "MariagePacs":
                    motif = "Mariage ou Pacs (4 jours ouvrés)";
                    break;
                default:
                motif = "";
                    break;
            }
            
            string mergeFieldMotif = motif;


            return View();
        }


        public ActionResult ChargerDocAutorisation()
        {
            string racine = _env.ContentRootPath;
           //recuperer modele
            string path = Path.Combine(racine, "ModelesOffice\\Autorisation.docx");
            // destination avec nom unique
            string destinationPath = Path.Combine(racine, @$"AutorisationAbsence\Autorisation{(DateTime.Now-DateTime.MinValue).TotalMinutes}.docx");
            //Copie du fichier avec possibilite d'écrire"
            System.IO.File.Copy(path, destinationPath, true);
            //Remplacer les MergeFields par valeurs
            using (WordprocessingDocument document=WordprocessingDocument.Open(destinationPath, true))
            {
                var mergeFields = document.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Nom").ReplaceWithText("SIRE");
                mergeFields.WhereNameIs("Prenom").ReplaceWithText("Romain");
                mergeFields.WhereNameIs("Formation").ReplaceWithText("CDA");
                mergeFields.WhereNameIs("DateDuJour").ReplaceWithText($"{DateTime.Today}");
                mergeFields.WhereNameIs("DateDebut").ReplaceWithText($"{DateTime.Today}");
                mergeFields.WhereNameIs("DateFin").ReplaceWithText($"{DateTime.Today}");
                mergeFields.WhereNameIs("NbJour").ReplaceWithText("0");

                document.MainDocumentPart.Document.Save();
                
            }
           // ContentDisposition content = new ContentDisposition()
           // { FileName=}
            return View();

            

            //Ouvrir le package de documents Word
            //WordprocessingDocument document = WordprocessingDocument.Open("C:\\Users\\CDA\\Desktop\\Projet_Mars2021\\AppAfpaBrive.Web\\ModelesOffice\\AutorisationAbsence.docx", true);

            //Acceder au corps de la partie principale du document
           // Body body = document.MainDocumentPart.Document.Body;
            //return View();
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
