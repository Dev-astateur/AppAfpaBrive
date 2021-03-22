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
        //public PeeController()
        //{
        //    //_dbContext = context;

        //    //_peeLayer = new PeeLayer(context);
        //    ////_paysLayer = new PaysLayer(context);    //-- pour test
        //}
        public PeeController(AFPANADbContext context, IConfiguration config, IHostEnvironment env)
        {
            _dbContext = context;
            _config = config;
            _env = env;
            _peeLayer = new PeeLayer(context);
        }
        #endregion
        #region Méthode IAction Index et AfficheBeneficiairePee
        /// <summary>
        /// IAction Index pour rechercher les stagiaire et 
        /// leurs Pee avec IdOffreFormation et IdEtablissement
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        /// <summary>
        /// IAction pour afficher le nom et le prénom 
        /// des beneficiaire et les entreprise qui les 
        /// accueillent et leurs Pee
        /// </summary>
        /// <param name="IdOffreFormation"></param>
        /// <param name="idEtablissement"></param>
        /// <returns></returns>
        public IActionResult _AfficheBeneficiairePee(int IdOffreFormation, string idEtablissement)
        {
            var pees = _peeLayer.GetPeeEntrepriseWithBeneficiaire(IdOffreFormation, idEtablissement);
            var listPeriode = _peeLayer.GetListPeriodePeeByIdPee(IdOffreFormation, idEtablissement);
            ViewData["PeriodePee"] = listPeriode;
            IEnumerable<Pee> PeeSansDoublons = pees.Distinct(new PeeComparer());
            ViewData["ListPeeSansDoublons"] = PeeSansDoublons;
            return View ("Index");
        }
        #endregion
        #region IAction pour inserer les données de la convention Pee du beneficiaire et de la charger pour impression ou téléchargement
        /// <summary>
        /// IAction pour inserer les données de la convention Pee de beneficiaire et de la charger pour impression ou téléchargement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ChargementDocPee(int id)
        {
            string path = Path.Combine(_env.ContentRootPath, "ModelesOffice");

            var Convention = _dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation)
                .ThenInclude(S => S.CodeTitreCiviliteNavigation)
                .Include(pee => pee.IdResponsableJuridiqueNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(t => t.IdTuteurNavigation)
                .ThenInclude(T => T.TitreCiviliteNavigation)
                .Include(P => P.IdEntrepriseNavigation)
                .Include(E => E.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation)
                .FirstOrDefault(pee => pee.IdPee == id);
            var Fonction = _dbContext.EntrepriseProfessionnels.FirstOrDefault(F => F.IdProfessionnel == Convention.IdResponsableJuridique);
            string Civilite = Convention.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet;
            var Collaborateur = _dbContext.OffreFormations.Include(O => O.MatriculeCollaborateurAfpaNavigation)
                .Where(C => C.IdOffreFormation == Convention.IdOffreFormation && C.IdEtablissement == Convention.IdEtablissement).FirstOrDefault();
            string pathModele = Civilite == "Monsieur" ? Path.Combine(path, "1-ConventionPE-M.docx") : Path.Combine(path, "1-ConventionPE-F.docx");
            var date = _dbContext.PeriodePees.FirstOrDefault(p => p.IdPee == Convention.IdPee);

            string nomFichier = $"{Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{Convention.IdEtablissement}-{Convention.IdOffreFormation}.docx";
            string docPath = @$"{path}\Convention_{(DateTime.Now - DateTime.MinValue).TotalMilliseconds}.docx";

            System.IO.File.Copy($"{pathModele}", docPath, true);
            using (WordprocessingDocument document = WordprocessingDocument.Open(docPath, true))
            {
                var mergeFields = document.GetMergeFields().ToList();
                mergeFields.WhereNameIs("Entreprise").ReplaceWithText(Convention.IdEntrepriseNavigation.RaisonSociale);
                mergeFields.WhereNameIs("Adresse1").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne1Adresse);
                mergeFields.WhereNameIs("Adresse2").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne2Adresse);
                mergeFields.WhereNameIs("Adresse3").ReplaceWithText(Convention.IdEntrepriseNavigation.Ligne3Adresse);
                mergeFields.WhereNameIs("Code_Postal").ReplaceWithText(Convention.IdEntrepriseNavigation.CodePostal);
                mergeFields.WhereNameIs("Commune").ReplaceWithText(Convention.IdEntrepriseNavigation.Ville);
                mergeFields.WhereNameIs("Tél_").ReplaceWithText(Convention.IdEntrepriseNavigation.TelEntreprise);
                mergeFields.WhereNameIs("TITRE_REP").ReplaceWithText(Convention.IdResponsableJuridiqueNavigation.TitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Représentant").ReplaceWithText(Convention.IdResponsableJuridiqueNavigation.NomProfessionnel + " " + Convention.IdResponsableJuridiqueNavigation.PrenomProfessionnel);
                mergeFields.WhereNameIs("Fonction_Représentant").ReplaceWithText(Fonction.Fonction);
                mergeFields.WhereNameIs("Titre_Tuteur_1").ReplaceWithText(Convention.IdTuteurNavigation.TitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Tuteur1").ReplaceWithText(Convention.IdTuteurNavigation.NomProfessionnel + " " + Convention.IdTuteurNavigation.PrenomProfessionnel);
                mergeFields.WhereNameIs("NOM_Stagiaire").ReplaceWithText(Convention.MatriculeBeneficiaireNavigation.NomBeneficiaire);
                mergeFields.WhereNameIs("Prénom_Stagiaire").ReplaceWithText(Convention.MatriculeBeneficiaireNavigation.PrenomBeneficiaire);
                mergeFields.WhereNameIs("Titre_Stagiaire").ReplaceWithText(Civilite);
                mergeFields.WhereNameIs("Titre_Formateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet);
                mergeFields.WhereNameIs("Formateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.PrenomCollaborateur + " " + Collaborateur.MatriculeCollaborateurAfpaNavigation.NomCollaborateur);
                mergeFields.WhereNameIs("TélFormateur").ReplaceWithText(Collaborateur.MatriculeCollaborateurAfpaNavigation.TelCollaborateurAfpa);
                mergeFields.WhereNameIs("Début_stage").ReplaceWithText($"{date.DateDebutPeriodePee}");
                mergeFields.WhereNameIs("Fin_stage").ReplaceWithText($"{date.DateFinPeriodePee}");
                Settings settings = document.MainDocumentPart.DocumentSettingsPart.Settings;
                foreach (var element in settings.ChildElements)
                {
                    if (element is MailMerge)
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
        #endregion
        [HttpGet]
        public IActionResult ListePeeAValider(string id)
        {
            this.ViewBag.Titre = "Periode en entreprise à valider";
            IEnumerable<Pee> pees = _peeLayer.GetPeeByMatriculeCollaborateurAfpa(id);
            List<PeeModelView> peesModelView = new();

            foreach (Pee item in pees)
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
