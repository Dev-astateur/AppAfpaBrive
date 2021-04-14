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
using System.Text;
using AppAfpaBrive.Web.Data;
using AppAfpaBrive.Web.Layers;

namespace AppAfpaBrive.Web.Controllers
{
    public class AutorisationAbsenceController : Controller
    {
        
       private readonly Layer_AutorisationAbsence _autorisationAbsenceLayer;
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        public AutorisationAbsenceController(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
           
            _config = config;
            _env = env;
            _autorisationAbsenceLayer =  new Layer_AutorisationAbsence(context);
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
            // Test avec les donnees stockées dans appJSON 
            string motif = _config.GetSection("Motif").GetSection(motifAbsence).Value;
           
           
            
            string motifSpecial= Request.Form["motifSpecial"];

            if (!(DateTime.TryParse(dateDebut, out DateTime dateDebutConverti))|| !(DateTime.TryParse(dateFin, out DateTime dateFinConverti))||(dateFinConverti< dateDebutConverti))
            {
                //Signaler que les dates sont invalides
                ViewData["ErreurDates"] = "Il y a eu un probleme dans la conformité et/ou la cohérence des dates, merci de renseigner les informations de nouveau";
                return View();

            }
            else
            {
                
                if (motifAbsence== "Autre")
                {
                    motif = motifSpecial;
                }

                
               string racine = _env.ContentRootPath;

                //recuperer modele
                string path = Path.Combine(racine, "ModelesOffice\\AutorisationAvecMotif.docx");

                // destination avec nom unique
                string destinationPath = Path.Combine(racine, @$"ModelesOffice\Autorisation{(DateTime.Now - DateTime.MinValue).TotalMinutes}.docx");

                //Copie du fichier avec possibilite d'écrire"
                System.IO.File.Copy(path, destinationPath, true);


               // a decommenter dans la version finale, quand les bénéfiaciaaires pourront se connecter
              //  string matricule = User.Identity.Name;
                string matricule = "16174318";
                string nom = _autorisationAbsenceLayer.GetNomStagiaireByMatricule(matricule);
                string prenom = _autorisationAbsenceLayer.GetPrenomStagiaireByMatricule(matricule);
                string formation = _autorisationAbsenceLayer.GetFormationStagiaireByMatricule(matricule);

                //Remplacer les MergeFields par valeurs
                using (WordprocessingDocument document = WordprocessingDocument.Open(destinationPath, true))
                {
                    var mergeFields = document.GetMergeFields().ToList();
                    mergeFields.WhereNameIs("nom").ReplaceWithText(nom);
                    mergeFields.WhereNameIs("prenom").ReplaceWithText(prenom);
                    mergeFields.WhereNameIs("formation").ReplaceWithText(formation);
                    mergeFields.WhereNameIs("dateDuJour").ReplaceWithText(DateTime.Today.ToString("dd/MM/yyyy"));
                    mergeFields.WhereNameIs("dateDebut").ReplaceWithText(dateDebutConverti.ToString("dd/MM/yyyy"));
                    mergeFields.WhereNameIs("DateDeFin").ReplaceWithText(dateFinConverti.ToString("dd/MM/yyyy"));                   
                    mergeFields.WhereNameIs("motif").ReplaceWithText(motif);

                    document.MainDocumentPart.Document.Save();

                }             
                ContentDisposition content = new ContentDisposition()
                {
                    //FileName = NomFichier,
                    Inline = false

                };

                Response.Headers.Add("Content-Disposition", content.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                
                byte[] contenu = System.IO.File.ReadAllBytes(destinationPath);
                //J efface  le fichier copié
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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: AutorisationAbsence/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AutorisationAbsence/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AutorisationAbsence/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AutorisationAbsence/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AutorisationAbsence/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AutorisationAbsence/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AutorisationAbsence/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
