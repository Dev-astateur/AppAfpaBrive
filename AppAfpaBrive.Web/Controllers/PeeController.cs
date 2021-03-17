using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.ModelView;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenXmlHelpers.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Mime;
using Magnum.FileSystem;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AppAfpaBrive.Web.Controllers
{
    public class PeeController : Controller
    {
        #region champ privé
        private readonly PeeLayer _peeLayer = null;
        private readonly AFPANADbContext _dbContext;
        private readonly IConfiguration _config;

        private readonly IHostEnvironment _env;


        #endregion
        #region Constructeur
        //public PeeController(AFPANADbContext context)
        //{ 
        //    _dbContext = context;

        //    _peeLayer = new PeeLayer(context);
        //    //_paysLayer = new PaysLayer(context);    //-- pour test
        //}
        public PeeController(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
            _dbContext = context;
            _config = config;
            _env = env;
            _peeLayer = new PeeLayer(context);
        }
        #endregion

        public IActionResult Index(int IdOffreFormation=0, string idEtablissement=null)
        {
           
            return View();
        }
        
        public  IActionResult AfficheBeneficiairePee(int IdOffreFormation, string idEtablissement)
        {

            IQueryable<Pee> pees = _dbContext.Pees
                .Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(p => p.Pees).ThenInclude(S => S.IdEntrepriseNavigation)
                .Where(P => P.IdOffreFormation == IdOffreFormation && P.IdEtablissement == idEtablissement);
            IQueryable<PeriodePee> periodePees = _dbContext.PeriodePees.Include(pr => pr.IdPeeNavigation)
                .ThenInclude(pee => pee.MatriculeBeneficiaireNavigation)
                .Include(pr => pr.IdPeeNavigation)
                .Where(pee => pee.IdPeeNavigation.IdOffreFormation == IdOffreFormation && pee.IdPeeNavigation.IdEtablissement == idEtablissement);
            
                
            return View( pees); 
        }

        public IActionResult ChargementDocPee(int idPee)
        {
            string path = Path.Combine(_env.ContentRootPath, "ModelesOffice");

            string pathModele = Path.Combine(path, "1-ConventionPE.docx");

            // var pee = _context.Pees.Find((decimal)idPee);


            var Convention = _dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(S => S.CodeTitreCiviliteNavigation)
                .Include(pee => pee.IdResponsableJuridiqueNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation.TitreCiviliteComplet)
                .Include(E => E.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation.TitreCiviliteComplet)
                .Include(P => P.PeriodePees).FirstOrDefault(pee => pee.IdPee == idPee);
            var date = Convention.PeriodePees.FirstOrDefault(p => p.IdPee == idPee);
            string nomFichier = $"{Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{Convention.IdEtablissement}-{Convention.IdOffreFormation}.docx";
            string docPath = @$"{path}\Convention{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx";

            System.IO.File.Copy($"{pathModele}", docPath, true);
            using (WordprocessingDocument document = WordprocessingDocument.Open(docPath, true))
            {
                var mergeFields = document.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Nom_Entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.RaisonSociale);
                mergeFields.WhereNameIs("Adresse_entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne1Adresse);
                mergeFields.WhereNameIs("Adresse_entreprise_suite").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne2Adresse + " " + Convention.IdEntrepriseNavigation.Ligne3Adresse);
                mergeFields.WhereNameIs("Code_postal_entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.CodePostal);
                mergeFields.WhereNameIs("Ville_entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.Ville);
                mergeFields.WhereNameIs("Téléphone_entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.TelEntreprise);
                mergeFields.WhereNameIs("Titre_signataire").ReplaceWithText(Convention.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Prénom_signataire").ReplaceWithText(Convention.IdResponsableJuridiqueNavigation.PrenomProfessionnel);
                mergeFields.WhereNameIs("Nom_signataire").ReplaceWithText(Convention.IdResponsableJuridiqueNavigation.NomProfessionnel);
                mergeFields.WhereNameIs("Nom_signataire").ReplaceWithText(Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire);
                mergeFields.WhereNameIs("Prénom_signataire").ReplaceWithText(Convention.MatriculeBeneficiaireNavigation.PrenomBeneficiaire);
                mergeFields.WhereNameIs("Début_PAE").ReplaceWithText($"{date.DateDebutPeriodePee}");
                mergeFields.WhereNameIs("FIN_PAE").ReplaceWithText($"{date.DateFinPeriodePee}");
                Settings settings = document.MainDocumentPart.DocumentSettingsPart.Settings;
                foreach(var element in settings.ChildElements)
                {
                    if(element is MailMerge)
                    {
                        element.Remove();
                    }

                }
                document.MainDocumentPart.Document.Save();

            }
            ContentDisposition content = new ContentDisposition()
            {
                FileName = nomFichier,

                Inline = false  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", content.ToString());

            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            byte[] contenu = System.IO.File.ReadAllBytes(docPath);
            
            System.IO.File.Delete(docPath);

            return File(contenu, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            
        }
        [HttpGet]
        public IActionResult ListePeeAValider(string id)
        {
            this.ViewBag.Titre = "Periode en entreprise à valider";
            IEnumerable<Pee> pees = _peeLayer.GetPeeByMatriculeCollaborateurAfpa(id);
            List<PeeModelView> peesModelView = new();

            foreach (Pee item in pees )
            {
                peesModelView.Add(new PeeModelView(item));
            }
            return View(peesModelView);
        }
        /// <summary>
        /// IAction qui suit le système de validation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SuivantEntreprise(int id)
        {
            // données pour les tests faudra changé tous cela
            Pee pee = _peeLayer.GetPeeByIdPee(id);
            PeeModelView peeModelView = new PeeModelView(pee);
            return View(peeModelView);
        }

        /// <summary>
        /// Action d'enregistrement des remarques sur la période en entreprise
        /// </summary>
        /// <param name="id">id de Pee</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SuivantRemarques(int id)
        {
            return View();
        }

       
    }
}
