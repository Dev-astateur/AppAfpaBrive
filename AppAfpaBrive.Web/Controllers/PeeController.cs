using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
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
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Wordprocessing;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using AppAfpaBrive.Web.Utilitaires;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;

namespace AppAfpaBrive.Web.Controllers
{
    public class PeeController : Controller
    {
        #region champ privé
        private readonly PeeLayer _peeLayer = null;
        private readonly AFPANADbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHostEnvironment _env;
        private readonly IEmailSender _emailSender;


        #endregion

        #region Constructeur
        //public PeeController()
        //{
        //    //_dbContext = context;

        //    //_peeLayer = new PeeLayer(context);
        //    ////_paysLayer = new PaysLayer(context);    //-- pour test
        //}
        public PeeController(AFPANADbContext context, IConfiguration config, IHostEnvironment env, IEmailSender emailSender)
        {
            _dbContext = context;
            _config = config;
            _env = env;
            _peeLayer = new PeeLayer(context);
            _emailSender = emailSender;
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
            if (ModelState.IsValid)
            {
                var pees = _peeLayer.GetPeeEntrepriseWithBeneficiaireBy(IdOffreFormation, idEtablissement);
                var listPeriode = _peeLayer.GetListPeriodePeeByIdPee(IdOffreFormation, idEtablissement);
                ViewData["PeriodePee"] = listPeriode;
                IEnumerable<Pee> PeeSansDoublons = pees.Distinct(new PeeComparer());
                ViewData["ListPeeSansDoublons"] = PeeSansDoublons;
            }
            
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

        #region Validation des Pee par le formateur
        [HttpGet]
        public async Task<IActionResult> ListePeeAValider(string id,int? pageIndex)
        {
            if (id is null)
                return NotFound();
            if (pageIndex is null)
                pageIndex = 1;
            this.ViewBag.Titre = "Periode en entreprise à valider";
            //IEnumerable<ListePeeAValiderModelView> pees = ;  

            return View(await _peeLayer.GetPeeByMatriculeCollaborateurAfpaAsync(id,(int) pageIndex));
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnregistrementPeeInfo(int IdPee, PeeModelView peeModelView)
        {
            if (IdPee != peeModelView.IdPee)
                return NotFound();

            string MatriculeCollaborateurAfpa = await _peeLayer.GetPeeMatriculeFormateurByIdAsync(IdPee);

            if (peeModelView.IsValid)
            {
                try
                {   
                    peeModelView.Etat = EntityPOCOState.Modified;

                    if ( await _peeLayer.UpdatePeeAsync(peeModelView) )
                    {
                        return RedirectToAction(nameof(PrevenirBeneficaire), new {id = IdPee ,idColl = MatriculeCollaborateurAfpa });
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(ListePeeAValider), new { id = MatriculeCollaborateurAfpa });
                }
                catch(DbUpdateException)
                {
                    return RedirectToAction(nameof(ListePeeAValider), new { id = MatriculeCollaborateurAfpa });
                }
            }
            return RedirectToAction(nameof(ListePeeAValider),new { id = MatriculeCollaborateurAfpa });
        }

        /// <summary>
        /// IAction du controller qui fonctionne comme des web service
        /// ici on charge la partie de saisie des remarques s'il y a lien
        /// </summary>
        /// <returns></returns>
        [Route("/Pee/ListeDocumentPee/{id}")]
        [Route("/Pee/ListeDocumentPee/{id}/{page}")]
        [HttpGet]
        public async Task<IActionResult> ListeDocumentPee(decimal? id,int? page)
        {
            if (id is null)
                return NotFound();

            IEnumerable<PeeDocumentModelView> peeDocument = await _peeLayer.GetPeeDocumentByIdAsync((decimal)id);

            if ( page is not null )
            {
                return peeDocument.Count() == 0 ? RedirectToAction(nameof(PeeEntrepriseValidation), new { id })
                : PartialView("~/Views/Shared/Pee/_ListeDocumentPeePartial.cshtml", peeDocument);
            }
            else
            {
                return peeDocument.Count()==0 ? RedirectToAction(nameof(EnregistrementPeeInfo), new { id })
                : PartialView("~/Views/Shared/Pee/_ListeDocumentPeePartial.cshtml", peeDocument);
            }
        }

        [HttpGet]
        public async Task<IActionResult> PrevenirBeneficaire(decimal id,string idColl)
        {
            MessageModelView messageView = await _peeLayer.GetElementByIdPeeForMessageAsync(id);
            messageView.MatriculeCollaborateurAfpa = idColl;
            string sujet = "Notification de votre présence en entreprise";
            //string message = messageView.EtatPee == 0? _config.GetSection("MessagePee").GetSection("Non").GetSection("Message").Value:
            //    _config.GetSection("MessagePee").GetSection("Oui").GetSection("Message").Value;

            string message = messageView.EtatPee == 0 ? "Votre formateur n'a pas pu validé les documents et la saisie des informations pour la période en entreprise " :
                "Votre formateur a bien validé les documents et la saisie des informations ";

            message += "pour l'entreprise "+messageView.RaisonSociale+".";

            message += string.IsNullOrWhiteSpace(messageView.Remarque) ? "" : Environment.NewLine + Environment.NewLine + "Voici le message qui est spécifié: ";
            message += Environment.NewLine +"\""+ messageView.Remarque + "\"" + Environment.NewLine;
            message += Environment.NewLine + "Veuillez ne pas répondre à ce mail qui est envoyé automatiquement.";
            message += Environment.NewLine + "Si vous avez des questions, veuillez contacter votre formateur.";

            messageView.Message = message;
            await _emailSender.SendEmailAsync(messageView.MailBeneficiaire, sujet, message);
            return View(messageView);
        }
        #endregion
    }
}
 