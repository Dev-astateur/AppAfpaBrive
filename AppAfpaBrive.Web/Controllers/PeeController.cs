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
using AppAfpaBrive.Web.ModelView.ValidationPee;
using AppAfpaBrive.Web.Utilitaires;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text;
using Rotativa;
using Ionic.Zip;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private readonly string SessionIdOffreFormation = "IdOffreFormation";
        private readonly string SessionIdEtablissemnt = "idEtablissement";
        private readonly List<int> checkBox;

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
        public async Task<IActionResult> AfficheBeneficiairePee(int IdOffreFormation, string idEtablissement)
        {
             
            if (ModelState.IsValid)
            {
                var pees = await _peeLayer.GetPeeEntrepriseWithBeneficiaireBy(IdOffreFormation, idEtablissement);
                var listPeriode = await _peeLayer.GetListPeriodePeeByIdPee(IdOffreFormation, idEtablissement);
                
                
                ViewData["PeriodePee"] = listPeriode;
                IEnumerable<Pee> PeeSansDoublons = pees.Distinct(new PeeComparer());
                
                ViewData["ListPeeSansDoublons"] =  PeeSansDoublons;
                HttpContext.Session.SetString("MessageSuccess", "");
                HttpContext.Session.SetInt32(SessionIdOffreFormation, IdOffreFormation);
                HttpContext.Session.SetString(SessionIdEtablissemnt, idEtablissement);
            }
            
            return View("Index");
            
        }
        #endregion

        #region Document accompagnement Beneficiaire
        /// <summary>
        /// Action pour génerer et télécharger les documents d'accompagnement des stagiaires pendand la période en entreprise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PeecheckBox"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDocumentForPrint(int id, int[] PeecheckBox)
        {
            if(PeecheckBox.Length == 0)
            {
                var idOffre = HttpContext.Session.GetInt32(SessionIdOffreFormation);
                var IdEtablissemnt = HttpContext.Session.GetString(SessionIdEtablissemnt);
                ModelState.AddModelError("error", "Vous devez cochez au moins une case");
                return RedirectToAction("AfficheBeneficiairePee", new { idOffreFormation = idOffre, IdEtablissement = IdEtablissemnt });
            }
            ///PeecheckBox est un tableau des valeur des IdPee
            /// id est le Id du input 
            ImpressionFicheSuivi PrintWord = new(_dbContext, _env);
            byte[] contenu = null;
            FileStreamResult result;

            int value = 0;
            var outPutStream = new MemoryStream();
            string FileNameZip = null;
            string fichierDoc = null;
            List<string> ListFiles = new();
            Pee pee = new();
            string PathDoc = Path.Combine(_env.ContentRootPath);

            ///itération sur le tableau des Id des Pee via les checkBox
            for (int i = 0; i < PeecheckBox.Length; i++)
            {

                value = PeecheckBox[i];
                pee = await PrintWord.GetDataBeneficiairePeeById(value);
                ListFiles.AddRange(await PrintWord.GetPathFile(value, id));
            }
            ///s'il y a plusieurs Fichiers on crée un fichier Zip avec les fichiers Word
                    if (ListFiles.Count > 1)
                    {
                        using (var ZipDoc = new ZipFile())
                        {
                            FileNameZip = $"Document_Suivi_Pee_{value}";
                            for (int j = 0; j < ListFiles.Count; j++)
                            {

                                string nomFichier = $"{pee.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire}-{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{pee.IdEtablissement}-{pee.IdOffreFormation}";
                                ContentDisposition content = new()
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
                        ContentDisposition content = new()
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

        #region action pour avoir des traces des suivis de la période en entreprise
       
       
        /// <summary>
        /// action pour afficher la view pour la traçabilité des suivis des stagiaires en entreprise
        /// </summary>
        /// <param name="PeecheckBox"></param>
        /// <returns></returns>
        public IActionResult ConsignezLeSuiviDuPee(List<int> PeecheckBox)
        {
            
            
                List<Pee> ListPee = new();
                foreach (var item in PeecheckBox)
                {
                     ListPee.Add(_dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation).FirstOrDefault(p => p.IdPee == item));
                }
                HttpContext.Session.SetObjectAsJson("checkBox", PeecheckBox);
            var idOffreFormation = HttpContext.Session.GetInt32(SessionIdOffreFormation);
            var IdEtablissement = HttpContext.Session.GetString(SessionIdEtablissemnt);
            ViewBag.idOf = idOffreFormation;
            ViewBag.IdEtab = IdEtablissement;
            ViewBag.IdPee = ListPee;
            ViewBag.MessageSuccess = HttpContext.Session.GetString("MessageSuccess");
            ViewBag.objecValid = HttpContext.Session.GetString("ValidObjectSuivi");
                return View();

           
            

        }
        /// <summary>
        /// Action pour l'enregistrement des données du suivi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConsignezLeSuiviDuPee(PeriodePeeSuiviCreateViewModel model)
        {
            var listcheckBox = HttpContext.Session.GetObjectFromJson<List<int>>("checkBox");
            string filePath = null;
            if (ModelState.IsValid)
            {
                string onlyOneFileName = null;

                var numOrd = _dbContext.PeeDocuments.OrderBy(d => d.NumOrdre).Select(d => d.NumOrdre).LastOrDefault();


                if (model.Fichier != null)
                {
                    string uploadFolder = Path.Combine(_env.ContentRootPath, "wwwroot/UploadFiles/" + model.IdPee + "/");
                    Directory.CreateDirectory(uploadFolder);
                    onlyOneFileName = Guid.NewGuid().ToString() + "_" + model.Fichier.FileName;
                    filePath = Path.Combine(uploadFolder, onlyOneFileName);
                    await model.Fichier.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }


                PeriodePeeSuivi periodePeeSuivi = new()
                {
                    IdPee = model.IdPee,
                    DateDeSuivi = model.DateDeSuivi,
                    ObjetSuivi = model.ObjetSuivi,
                    TexteSuivi = model.TexteSuivi,


                };
                _dbContext.Add(periodePeeSuivi);
                await _dbContext.SaveChangesAsync();
                PeeDocument peeDocument = new()
                {
                    IdPee = model.IdPee,
                    IdPeriodePeeSuivi = _dbContext.PeriodePeeSuivis.OrderBy(p => p.IdPeriodePeeSuivi).Select(p => p.IdPeriodePeeSuivi).LastOrDefault(),
                    PathDocument = filePath,
                    NumOrdre = numOrd + 1

                };
                _dbContext.Add(peeDocument);
                await _dbContext.SaveChangesAsync();
                var idOffre = HttpContext.Session.GetInt32(SessionIdOffreFormation);
                var IdEtablissemnt = HttpContext.Session.GetString(SessionIdEtablissemnt);
                 HttpContext.Session.SetString("MessageSuccess", "Enregistrement réussi");
               

            }
            if(model.ObjetSuivi==null)
            {
                HttpContext.Session.SetString("ValidObjectSuivi", "Le champ est requis");
            }

            return RedirectToAction("ConsignezLeSuiviDuPee", new { PeecheckBox = listcheckBox });
        }

        #endregion
        #region consultation les document de suivi des la période en entreprise
        
        public IActionResult ConsultationSuivi(List<int> PeecheckBox)
        {
            List<Pee> selectListPee = new();
            foreach(var item in PeecheckBox)
            {
                selectListPee.AddRange(_dbContext.Pees
               .Where(d => d.IdPee == item)
               .Include(d => d.PeeDocument)
               .Include(p => p.PeriodePeeSuivis)
               .Include(b => b.MatriculeBeneficiaireNavigation));
            }
            HttpContext.Session.SetObjectAsJson("checkBox", PeecheckBox);
            var idOffre = HttpContext.Session.GetInt32(SessionIdOffreFormation);
            var IdEtablissemnt = HttpContext.Session.GetString(SessionIdEtablissemnt);
            ViewBag.ideOffre = idOffre;
            ViewBag.IdEtab = IdEtablissemnt;
            ViewBag.Pee = selectListPee;
            return View();
        }
        [HttpGet]
        #endregion
        public async Task<IActionResult> DownLoadDocument(int IdPeeDoc)
        {
            var listcheckBox = HttpContext.Session.GetObjectFromJson<List<int>>("checkBox");
            string pathDoc = VerifyFileExist(IdPeeDoc);
            bool fileExist = System.IO.File.Exists(pathDoc);
            if (pathDoc == null || fileExist == false)
            {
                return Content("Le fichier n'existe pas");
                
            }
            var memory = new MemoryStream();

            using(var stream = new FileStream(pathDoc, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(pathDoc), Path.GetFileName(pathDoc));

            
        }
        public string VerifyFileExist(int IdPeriodePeeSuivi)
        {
            var pathDoc = _dbContext.PeeDocuments.FirstOrDefault(p => p.IdPeriodePeeSuivi == IdPeriodePeeSuivi);
            bool fileExist = System.IO.File.Exists(pathDoc.PathDocument);
            if(pathDoc.PathDocument == null || fileExist == false)
            {
                return "Il n'y a pas de document à télécharger";
            }
            return pathDoc.PathDocument;
        }
        #region methode pour séléctionner le type des fichiers
        private static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".odt","application/vnd.oasis.opendocument.text" },
                {".rtf","application/rtf" },
                {".zip" ,"application/zip"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        #endregion
        #region méthode pour la séléction les objets du texte de suivi
        public List<SelectListItem> GetListObject(int IdPee)
        {
            var listObject = _dbContext.PeriodePeeSuivis.Include(o => o.PeeDocuments).Where(o => o.IdPee == IdPee).ToList();
            var listsuivi = listObject.Select(s => new SelectListItem() { Text = s.ObjetSuivi, Value = s.IdPeriodePeeSuivi.ToString() }).ToList();
             
            return listsuivi;
        }
        #endregion
        #region méthode pour le séléction le texte du suivi
        public PeriodePeeSuivi  GetPeriodePeeSuiviTextCkEditor(int IdPeriodePeeSuivi)
        {
            var suiviPeriodePee = _dbContext.PeriodePeeSuivis
                .Where(Pd => Pd.IdPeriodePeeSuivi == IdPeriodePeeSuivi).FirstOrDefault();
            
           
            return suiviPeriodePee; 
        }
        #endregion
        #region update du texte du suivi
        [HttpPost]
        public async Task<IActionResult> ConsultationSuivi(PeriodePeeSuiviCreateViewModel model)
        {
            var listcheckBox = HttpContext.Session.GetObjectFromJson<List<int>>("checkBox");
            var obj = _dbContext.PeriodePeeSuivis.FirstOrDefaultAsync(p => p.IdPeriodePeeSuivi == model.IdPeriodePeeSuivi).Result;
            obj.TexteSuivi = model.TexteSuivi;
            _dbContext.Update(obj);
           await _dbContext.SaveChangesAsync();
            return RedirectToAction("ConsultationSuivi", new { PeecheckBox = listcheckBox });
        }
        #endregion

        #region Validation des Pee par le formateur
        /// <summary>
        /// liste des Pee par formateur
        /// </summary>
        /// <param name="id">id du matricule du formateur</param>
        /// <param name="pageIndex">page à afficher non obligatoire</param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<IActionResult> ListePeeAValider(string id,int? pageIndex)
        {
            if (string.IsNullOrWhiteSpace(id) || id == "")
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
                return !peeDocument.Any() ? RedirectToAction(nameof(PeeEntrepriseValidation), new { id })
                : PartialView("~/Views/Shared/Pee/_ListeDocumentPeePartial.cshtml", peeDocument);
            }
            else
            {
                return !peeDocument.Any() ? RedirectToAction(nameof(EnregistrementPeeInfo), new { id })
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
 