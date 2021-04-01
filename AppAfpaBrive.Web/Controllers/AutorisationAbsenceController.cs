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
        public ActionResult CompleterInfoAbsence(string motifAbsence, string dateDebut, string dateFin)
        {
            
            string motif="";
           string motifSpecial= Request.Form["motifSpecial"];

            if (!(DateTime.TryParse(dateDebut, out DateTime dateDebutConverti))|| !(DateTime.TryParse(dateFin, out DateTime dateFinConverti))||(dateFinConverti< dateDebutConverti))
            {
                //Signaler que les dates sont invalides
                ViewData["ErreurDates"] = "Il y a eu un probleme dans la conformité et/ou la cohérence des dates, merci de renseigner les informations de nouveau";
                return View();

            }
            else
            {
                //Je dois recuperer le motif d'absence choisi dans les radioButtons
                switch (motifAbsence)
                {
                    case "JournéeDefense":
                        motif = "Journée défense et citoyenneté / Céremonie d'accueil dans la citoyenneté (1 jour ouvré)";
                        break;
                    case "MariagePacs":
                        motif = "Mariage ou Pacs (4 jours ouvrés)";
                        break;
                    case "Naissance":
                        motif = "Naissance ou adoption (3 jours ouvrés)";
                        break;
                    case "MariageEnfant":
                        motif = "Mariage d'un enfant (1 jour ouvré)";
                        break;
                    case "DecesEnfant":
                        motif = "Décès d'un enfant (5 jours ouvrés)";
                        break;
                    case "Deces":
                        motif = "Décès d'un conjoint, père, mère, beau-père, belle-mère, frère ou soeur du stagiaire";
                        break;
                    case "EnfantMalade":
                        motif = "Enfant malade de moins de 16 ans (3 jours ouvrés sur la durée de la formation)";
                        break;
                    case "ExamPrenatal":
                        motif = "Absence pour examen prénatal de grossesse obligatoire à compter du 3ème mois de grossesse";
                        break;
                    case "HandicapEnfant":
                        motif = "Annonce de la survenue d'un handicap chez un enfant (2 jours ouvrés)";
                        break;
                    case "FeteReligieuse":
                        motif = "Absence pour fêtes religieuses hors jours fériés légaux";
                        break;
                    case "GreveDesTransports":
                        motif = "Grève des transports";
                        break;
                    case "Intemperies":
                        motif = "Intempéries";
                        break;
                    case "RdvMilitaire":
                        motif = "Rdv avec le conseiller militaire";
                        break;
                    case "RechercheLogement":
                        motif = "Recherche de logement, rdv organismes divers";
                        break;
                    case "Autre":
                        motif = motifSpecial;
                        break;


                }

                string racine = _env.ContentRootPath;
                //recuperer modele
                string path = Path.Combine(racine, "ModelesOffice\\AutorisationAvecMotif.docx");
                // destination avec nom unique
                string destinationPath = Path.Combine(racine, @$"AutorisationAbsence\Autorisation{(DateTime.Now - DateTime.MinValue).TotalMinutes}.docx");
                //Copie du fichier avec possibilite d'écrire"
                System.IO.File.Copy(path, destinationPath, true);
                //Remplacer les MergeFields par valeurs
                using (WordprocessingDocument document = WordprocessingDocument.Open(destinationPath, true))
                {
                    var mergeFields = document.GetMergeFields().ToList();
                    mergeFields.WhereNameIs("nom").ReplaceWithText("SIRE");
                    mergeFields.WhereNameIs("prenom").ReplaceWithText("Romain");
                    mergeFields.WhereNameIs("formation").ReplaceWithText("CDA");
                    mergeFields.WhereNameIs("dateDuJour").ReplaceWithText(DateTime.Today.ToString("dd/MM/yyyy"));
                    mergeFields.WhereNameIs("dateDebut").ReplaceWithText(dateDebutConverti.ToString("dd/MM/yyyy"));
                    mergeFields.WhereNameIs("DateDeFin").ReplaceWithText(dateFinConverti.ToString("dd/MM/yyyy")); 
                    mergeFields.WhereNameIs("motif").ReplaceWithText(motif);

                    document.MainDocumentPart.Document.Save();

                }
                //Mttre le document en piece jointe puis le supprimer
                //string NomFichier = $"NomPrenom{DateTime.Today.ToString("dd/MM/yyyy")}{motifAbsence}";

                ContentDisposition content = new ContentDisposition()
                {
                    //FileName = NomFichier,
                    Inline = false

                };

                Response.Headers.Add("Content-Disposition", content.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                //Attention de s'assurer qu'il n'y ait pas d'autres fichiers dans destinationPath?
                byte[] contenu = System.IO.File.ReadAllBytes(destinationPath);
                System.IO.File.Delete(destinationPath);
                return File(contenu, "application/vnd.openxmlformats-officedocument.wordprocessingml.document",$"AutorisationAbsenceNomPrenom.docx");
            }
          
           
        }


        //public ActionResult ChargerDocAutorisation()
        //{
        //    string racine = _env.ContentRootPath;
        //   //recuperer modele
        //    string path = Path.Combine(racine, "ModelesOffice\\Autorisation.docx");
        //    // destination avec nom unique
        //    string destinationPath = Path.Combine(racine, @$"AutorisationAbsence\Autorisation{(DateTime.Now-DateTime.MinValue).TotalMinutes}.docx");
        //    //Copie du fichier avec possibilite d'écrire"
        //    System.IO.File.Copy(path, destinationPath, true);
        //    //Remplacer les MergeFields par valeurs
        //    using (WordprocessingDocument document=WordprocessingDocument.Open(destinationPath, true))
        //    {
        //        var mergeFields = document.GetMergeFields().ToList();
        //        mergeFields.WhereNameIs("Nom").ReplaceWithText("SIRE");
        //        mergeFields.WhereNameIs("Prenom").ReplaceWithText("Romain");
        //        mergeFields.WhereNameIs("Formation").ReplaceWithText("CDA");
        //        mergeFields.WhereNameIs("DateDuJour").ReplaceWithText($"{DateTime.Today}");
        //        mergeFields.WhereNameIs("DateDebut").ReplaceWithText($"{DateTime.Today}");
        //        mergeFields.WhereNameIs("DateFin").ReplaceWithText($"{DateTime.Today}");
        //        mergeFields.WhereNameIs("NbJour").ReplaceWithText("0");

        //        document.MainDocumentPart.Document.Save();
                
        //    }
        //   // ContentDisposition content = new ContentDisposition()
        //   // { FileName=}
        //    return View();

            

        //    //Ouvrir le package de documents Word
        //    //WordprocessingDocument document = WordprocessingDocument.Open("C:\\Users\\CDA\\Desktop\\Projet_Mars2021\\AppAfpaBrive.Web\\ModelesOffice\\AutorisationAbsence.docx", true);

        //    //Acceder au corps de la partie principale du document
        //   // Body body = document.MainDocumentPart.Document.Body;
        //    //return View();
        //}
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
