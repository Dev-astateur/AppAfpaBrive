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
