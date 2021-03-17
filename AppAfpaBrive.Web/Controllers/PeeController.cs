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
        private readonly PaysLayer _paysLayer = null;
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
            //_paysLayer = new PaysLayer(context);    //-- pour test
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

       
    }
}
