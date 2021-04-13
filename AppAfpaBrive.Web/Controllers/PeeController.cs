
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
using DocumentFormat.OpenXml.Wordprocessing;
using AppAfpaBrive.Web.Utilitaires;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using Rotativa;
using Ionic.Zip;
using System.Text.RegularExpressions;
using AppAfpaBrive.Web.ModelView.ValidationPee;
using Microsoft.AspNetCore.Authorization;

namespace AppAfpaBrive.Web.Controllers
{
    [Authorize(Roles= "Formateur")]
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
        public async Task<IActionResult> _AfficheBeneficiairePee(int IdOffreFormation, string idEtablissement)
        {
            if (ModelState.IsValid)
            {
                var pees = await _peeLayer.GetPeeEntrepriseWithBeneficiaireBy(IdOffreFormation, idEtablissement);
                var listPeriode = await _peeLayer.GetListPeriodePeeByIdPee(IdOffreFormation, idEtablissement);
                ViewData["PeriodePee"] = listPeriode;
                IEnumerable<Pee> PeeSansDoublons = pees.Distinct(new PeeComparer());
                ViewData["ListPeeSansDoublons"] =  PeeSansDoublons;
               
            }
            
            return View ("Index");
        }
        #endregion

        #region Document accompagnement Beneficiaire

        public async Task<IActionResult> GetDocumentForPrint(int id, int[] PeecheckBox)
        {
            ///PeecheckBox est un tableau des valeur des IdPee
            /// id est le Id du input 
            Layer_ImpressionFicheSuivi PrintWord = new Layer_ImpressionFicheSuivi(_dbContext, _env);
            byte[] contenu = null;
            FileStreamResult result;

            int value = 0;
            var outPutStream = new MemoryStream();
            string FileNameZip = null;
            string fichierDoc = null;
            List<string> ListFiles = new List<string>();
            Pee pee = new Pee();
            string PathDoc = Path.Combine(_env.ContentRootPath);

            ///itération sur le tableau des Id des Pee via les checkBox
            for (int i = 0; i < PeecheckBox.Length; i++)
            {

                value = PeecheckBox[i];
                pee = await PrintWord.GetDataBeneficiairePeeById(value);
                ListFiles.AddRange(await PrintWord.GetPathFile(value, id));
            }
            ///s'il y a plusieurs Fichiers on crée un fichier Zip avec les fichiers Word
                    if (ListFiles.Count() > 1)
                    {
                        using (var ZipDoc = new ZipFile())
                        {
                            FileNameZip = $"Document_Suivi_Pee_{value}";
                            for (int j = 0; j < ListFiles.Count(); j++)
                            {

                                string nomFichier = $"{pee.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire}-{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{pee.IdEtablissement}-{pee.IdOffreFormation}";
                                ContentDisposition content = new ContentDisposition()
                                {
                                    FileName = $"{nomFichier}.docx",

                                    Inline = false
                                };
                                Response.Headers.Add($"Content-Disposition-{j}_{value}", content.ToString());
                                Response.Headers.Add($"X-Content-Type-Options-{j}_{value}", "nosniff");
                                string doc = Path.GetFileName(ListFiles[j]);
                                string regexPath = Path.GetFileNameWithoutExtension(Regex.Replace(doc, @"[0-9,-]", ""));
                                doc = $"{regexPath}_{nomFichier}_{j + 1}.docx";
                                System.IO.File.Copy(ListFiles[j], doc);
                                ZipDoc.AddFile(doc);
                                System.IO.File.Delete(ListFiles[j]);

                            }
                            ZipDoc.Save(outPutStream);
                            ///Suppression les fichiers copier. 
                            Directory.GetFiles(PathDoc, "*.docx", SearchOption.TopDirectoryOnly).ToList().ForEach(System.IO.File.Delete);

                        }
                        outPutStream.Position = 0;
                        result = File(outPutStream, "application/zip", $"{FileNameZip}.zip");
                        ///envoie du fichier Zip qui contient les Document à telecharger
                        return result;
                    }
                    else  //sinon on télécharger le fichier Word
                    {

                        pee = await PrintWord.GetDataBeneficiairePeeById(value);
                        string nomFichier = $"{value}_{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}_{pee.MatriculeBeneficiaireNavigation.PrenomBeneficiaire}";

                        foreach (var item in ListFiles)
                        {
                            fichierDoc = item;

                        }
                        ContentDisposition content = new ContentDisposition()
                        {
                            FileName = nomFichier,

                            Inline = false
                        };
                        contenu = System.IO.File.ReadAllBytes(fichierDoc);
                        Response.Headers.Add("Content-Disposition1", content.ToString());

                        Response.Headers.Add("X-Content-Type-Options1", "nosniff");


                        System.IO.File.Delete(fichierDoc);
                        return File(contenu, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{content.FileName}.docx", true);
                    }
                

           
        }
        #endregion

        #region Validation des Pee par le formateur
        /// <summary>
        /// liste des Pee par formateur
        /// </summary>
        /// <param name="pageIndex">page à afficher non obligatoire</param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<IActionResult> ListePeeAValider(int? pageIndex)
        {
            string matriculeCollaborateur = User.Identity.Name;
            if (string.IsNullOrWhiteSpace(matriculeCollaborateur) || matriculeCollaborateur == "")
                return NotFound();

            if (pageIndex is null)
                pageIndex = 1;
            this.ViewBag.Titre = "Periode en entreprise à valider";
            //IEnumerable<ListePeeAValiderModelView> pees = ;  

            return View(await _peeLayer.GetPeeByMatriculeCollaborateurAfpaAsync(matriculeCollaborateur, (int) pageIndex));
        }

        /// <summary>
        /// Page principale de validation de Pee
        /// </summary>
        /// <param name="id">id IdPee, id de la Pee</param>
        /// <returns></returns>
        [Route("/Pee/PeeEntrepriseValidation/{id}")]
        [HttpGet]
        public async Task<IActionResult> PeeEntrepriseValidation(decimal? id)
        {
            if ( id is null )
                return NotFound();

            PeeEntrepriseModelView pee = await _peeLayer.GetPeeByIdPeeOffreEntreprisePaysAsync((decimal)id);
            if (pee is null)
                return BadRequest();

            return View(pee);
        }

        /// <summary>
        /// Iaction du controller qui fonctionne comme des web service
        /// ici on charge la partie de saisie des remarques s'il y a lien
        /// </summary>
        /// <param name="id">id IdPee, id de la Pee</param>
        /// <returns>charge la page de modification de la Pee</returns>
        [Route("/Pee/EnregistrementPeeInfo/{id}")]
        [HttpGet]
        public async Task<IActionResult> EnregistrementPeeInfo(decimal? id)
        {
            if (id is null)
                return NotFound();

            PeeModelView pee = await _peeLayer.GetPeeByIdAsync((decimal)id);
            if (pee is null)
                return BadRequest();

            return PartialView("~/Views/Shared/Pee/_AddRemarque.cshtml",pee) ;
        }

        /// <summary>
        /// action du update de la Pee
        /// </summary>
        /// <param name="IdPee"></param>
        /// <param name="peeModelView"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnregistrementPeeInfo(decimal IdPee, PeeModelView peeModelView)
        {
            if (IdPee != peeModelView.IdPee)
                return BadRequest();

            string MatriculeCollaborateurAfpa = await _peeLayer.GetPeeMatriculeFormateurByIdAsync(IdPee);

            if (peeModelView.IsValid)
            {
                try
                {   
                    peeModelView.Etat = EntityPOCOState.Modified;

                    await _peeLayer.UpdatePeeAsync(peeModelView);
                    return RedirectToAction(nameof(PrevenirBeneficaire), new {id = IdPee ,idColl = MatriculeCollaborateurAfpa });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
                catch(DbUpdateException)
                {
                    return BadRequest();
                }
                catch (Exception)
                {
                    return BadRequest();
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

        /// <summary>
        /// action qui envoi un courriel au bénéficiaire
        /// </summary>
        /// <param name="id">idPee</param>
        /// <param name="idColl">id collaborateur Afpa</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PrevenirBeneficaire(decimal id,string idColl)
        {
            MessageModelView messageView = await _peeLayer.GetElementByIdPeeForMessageAsync(id);
            messageView.MatriculeCollaborateurAfpa = idColl;
            messageView.MessagePee = _config.GetSection("MessagePee").Get<MessagePee>();

            try
            {
                await _emailSender.SendEmailAsync(messageView.MailBeneficiaire, messageView.MessagePee.Sujet.Normalize(), messageView.Message.Normalize());
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return View(messageView);
        }
        #endregion
    }
}
 