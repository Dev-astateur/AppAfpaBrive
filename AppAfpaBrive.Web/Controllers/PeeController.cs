using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
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
using AppAfpaBrive.Web.ModelView.ValidationPee;
using AppAfpaBrive.Web.Layers;

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
        
        public IActionResult AfficheBeneficiairePee(int IdOffreFormation, string idEtablissement)
        {

            IQueryable<Pee> pees = _dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation).ThenInclude(p => p.Pees).ThenInclude(S => S.IdEntrepriseNavigation).Where(P => P.IdOffreFormation == IdOffreFormation && P.IdEtablissement == idEtablissement);

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
                .ThenInclude(T => T.CodeTitreCiviliteProfessionnel)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.CodeTitreCiviliteProfessionnel)
                .Include(E => E.IdEntrepriseNavigation).FirstOrDefault(pee => pee.IdPee == idPee);
                
            string nomFichier = $"{Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{Convention.IdEtablissement}-{Convention.IdOffreFormation}.docx";
            string docPath = @$"{path}\Convention{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx";

            System.IO.File.Copy($"{pathModele}", docPath, true);
            using (WordprocessingDocument document = WordprocessingDocument.Open(docPath, true))
            {
                var mergeFields = document.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Nom_Entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.RaisonSociale);
                mergeFields.WhereNameIs("Adresse_entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne1Adresse);
                mergeFields.WhereNameIs("Adresse_entreprise_suite").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne2Adresse + " " + Convention.IdEntrepriseNavigation.Ligne3Adresse);
                
                

                //document.MainDocumentPart.Document.Save();
            }

            return View(viewName: "AfficheBeneficiairePee");
        }

        [HttpGet]
        public async Task<IActionResult> ListePeeAValider(string id)
        {
            if (id is null)
                return NotFound();

            this.ViewBag.Titre = "Periode en entreprise à valider";
            IEnumerable<ListePeeAValiderModelView> pees = await _peeLayer.GetPeeByMatriculeCollaborateurAfpaAsync(id);  

            return View(pees);
        }

        /// <summary>
        /// Page principale de validation de Pee
        /// </summary>
        /// <returns></returns>
        [Route("/Pee/PeeEntrepriseValidation/{id}")]
        [HttpGet]
        public async Task<IActionResult> PeeEntrepriseValidation(int? id)
        {
            if ( id is null )
                return NotFound();

            PeeEntrepriseModelView pee = await _peeLayer.GetPeeByIdPeeOffreEntreprisePaysAsync((int)id);
            if (pee is null)
                return NotFound();

            return View(pee);
        }
       
        /// <summary>
        /// Iaction du controller qui fonctionne comme des web service
        /// ici on charge la partie de saisie des remarques s'il y a lien
        /// </summary>
        /// <returns></returns>
        [Route("/Pee/EnregistrementPeeInfo/{id}")]
        [HttpGet]
        public async Task<IActionResult> EnregistrementPeeInfo(int? id)
        {
            if (id is null)
                return NotFound();

            PeeModelView pee = await _peeLayer.GetPeeByIdAsync((int)id);
            return PartialView("~/Views/Shared/Pee/_AddRemarque.cshtml",pee) ;
        }

        [HttpPost]
        public async Task<IActionResult> EnregistrementPeeInfo(int IdPee, PeeModelView peeModelView)
        {
            if (IdPee != peeModelView.IdPee)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {

                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return RedirectToAction();
        }

        /// <summary>
        /// IAction du controller qui fonctionne comme des web service
        /// ici on charge la partie de saisie des remarques s'il y a lien
        /// </summary>
        /// <returns></returns>
        [Route("/Pee/ListeDocumentPee/{id}")]
        [HttpGet]
        public async Task<IActionResult> ListeDocumentPee(int? id)
        {
            if (id is null)
                return NotFound();

            PeeModelView pee = await _peeLayer.GetPeeByIdAsync((int)id);
            return PartialView("~/Views/Shared/Pee/_ListeDocumentPeePartial.cshtml", pee);
        }
    }
}
