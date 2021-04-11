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

        string SessionIdOffreFormation = "IdOffreFormation";
        string SessionIdEtablissemnt = "idEtablissement";
        List<int> checkBox = new List<int>();

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

                HttpContext.Session.SetInt32(SessionIdOffreFormation, IdOffreFormation);
                HttpContext.Session.SetString(SessionIdEtablissemnt, idEtablissement);
            }
            
            return View("Index");
            
        }
        #endregion

        #region Document accompagnement Beneficiaire
        
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
            ImpressionFicheSuivi PrintWord = new ImpressionFicheSuivi(_dbContext, _env);
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

        #region action pour avoir des traces des suivis de la période en entreprise
       
        

        public IActionResult ConsignezLeSuiviDuPee(List<int> PeecheckBox)
        {
            
            
                List<Pee> ListPee = new List<Pee>();
                foreach (var item in PeecheckBox)
                {
                    ListPee.Add(_dbContext.Pees.Include(P => P.MatriculeBeneficiaireNavigation).FirstOrDefault(p => p.IdPee == item));
                }
                HttpContext.Session.SetObjectAsJson("checkBox", PeecheckBox);
                ViewBag.IdPee = ListPee;
                return View();

           
            

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadCKEditor( IFormFile uploadFile)
        {
            
            var fileName =  DateTime.Now.ToString("yyyyMMddHHmmss") + uploadFile.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), _env.ContentRootPath, "wwwroot/UploadFiles/Images/", fileName);
            var stream = new FileStream(path, FileMode.Create);
            uploadFile.CopyToAsync(stream);
            return new JsonResult(new { path = "/UpLoadFiles/Images/" + fileName });
            
        }
        //[Route("upload_file")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePeriodePeeSuivi(PeriodePeeSuiviCreateViewModel model)
        {

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


                PeriodePeeSuivi periodePeeSuivi = new PeriodePeeSuivi
                {
                    IdPee = model.IdPee,
                    DateDeSuivi = model.DateDeSuivi,
                    ObjetSuivi = model.ObjetSuivi,
                    TexteSuivi = model.TexteSuivi,
                    

                };
                _dbContext.Add(periodePeeSuivi);
                await _dbContext.SaveChangesAsync();
                PeeDocument peeDocument = new PeeDocument
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
                
                return RedirectToAction("AfficheBeneficiairePee", new {idOffreFormation = idOffre, IdEtablissement = IdEtablissemnt });
            }
            var listcheckBox = HttpContext.Session.GetObjectFromJson<List<int>>("checkBox");

            return RedirectToAction("ConsignezLeSuiviDuPee", listcheckBox);
        }

        #endregion
        #region consultation les document de suivi des la période en entreprise
        
        public IActionResult ConsultationSuivi(List<int> PeecheckBox)
        {
            List<Pee> selectListPee = new List<Pee>();
            foreach(var item in PeecheckBox)
            {
                selectListPee.AddRange(_dbContext.Pees
               .Where(d => d.IdPee == item)
               .Include(d => d.PeeDocument)
               .Include(p => p.PeriodePeeSuivis)
               .Include(b => b.MatriculeBeneficiaireNavigation));
            }

            var idOffre = HttpContext.Session.GetInt32(SessionIdOffreFormation);
            var IdEtablissemnt = HttpContext.Session.GetString(SessionIdEtablissemnt);
            ViewBag.ideOffre = idOffre;
            ViewBag.IdEtab = IdEtablissemnt;
            ViewBag.Pee = selectListPee;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DownLoadDocument(int IdPeeDoc)
        {
            var pathDoc = _dbContext.PeeDocuments.FirstOrDefault(p => p.IdPeriodePeeSuivi == IdPeeDoc);
            if(pathDoc.PathDocument == null)
            {
                return Content("Fichier non présent");
            }
            var memory = new MemoryStream();
            using(var stream = new FileStream(pathDoc.PathDocument, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(pathDoc.PathDocument), Path.GetFileName(pathDoc.PathDocument));

            
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"}, 
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        public List<SelectListItem> GetListObject(int IdPee)
        {
            var listObject = _dbContext.PeriodePeeSuivis.Include(o => o.PeeDocuments).Where(o => o.IdPee == IdPee).ToList();
            var listsuivi = listObject.Select(s => new SelectListItem() { Text = s.ObjetSuivi, Value = s.IdPeriodePeeSuivi.ToString() }).ToList();
             
            return listsuivi;
        }
        public PeriodePeeSuivi  GetPeriodePeeSuiviTextCkEditor(int IdPeriodePeeSuivi)
        {
            var suiviPeriodePee = _dbContext.PeriodePeeSuivis
                .Where(Pd => Pd.IdPeriodePeeSuivi == IdPeriodePeeSuivi).FirstOrDefault();
            
           
            return suiviPeriodePee; 
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
 