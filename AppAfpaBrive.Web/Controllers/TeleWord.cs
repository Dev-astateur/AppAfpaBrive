using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlHelpers.Word;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AppAfpaBrive.Web.Controllers
{
    public class TeleWord : Controller
    {
        private readonly AFPANADbContext _context;
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        public TeleWord(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
            _context = context;
            _config = config;
            _env = env;
        }
        public IActionResult ChargementDocPee(int idPee)
        {
            string path = Path.Combine(_env.ContentRootPath, "ModelesOffice");
            string pathModele = Path.Combine(path, "2-PAE LETTRE envoi convention PAE.docx");
            var pee = _context.Pees.
                Include(pee => pee.MatriculeBeneficiaireNavigation).
                ThenInclude(ben => ben.CodeTitreCiviliteNavigation)
                .Include(pee=>pee.IdEntrepriseNavigation)
                .Include(pee=>pee.IdResponsableJuridiqueNavigation)
                .FirstOrDefault(pee => pee.IdPee == idPee);

            string nomFichier = $"{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{pee.IdEtablissement}-{pee.IdOffreFormation}.docx";

            string cible = @$"{path}\Convention{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx";
            System.IO.File.Copy($"{pathModele}", cible, true);
            using (WordprocessingDocument doc = WordprocessingDocument.Open(cible, true))
            {
                var mergeFields = doc.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Titre_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Prénom_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.PrenomBeneficiaire);
                mergeFields.WhereNameIs("NOM_Stagiaire").ReplaceWithText(pee.MatriculeBeneficiaireNavigation.NomBeneficiaire);
                // Suppression de la source de fusion
                Settings settings = doc.MainDocumentPart.DocumentSettingsPart.Settings;
            
                foreach (var element in settings.ChildElements)
                {
                    if (element is MailMerge)
                    {
                        element.Remove();
                    }
                }
                

                doc.MainDocumentPart.Document.Save();
            }
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = nomFichier,
                Inline = false  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            byte[] contenu = System.IO.File.ReadAllBytes(cible);
            System.IO.File.Delete(cible);
            return File(contenu, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");

        }
        // Response...


    }
}

