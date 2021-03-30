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
using Rotativa;
using Ionic.Zip;
using System.Text.RegularExpressions;

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
       
        #region Document accompagnement Beneficiaire

        public IActionResult GetDocumentForPrint(int id, int[] PeecheckBox)
        {

            ImpressionFicheSuivi PrintWord = new ImpressionFicheSuivi(_dbContext, _config, _env);
            byte[] contenu = null;
            
            int value = 0;
            var outPutStream = new MemoryStream();
            string FileNameZip = null;
            string fichierDoc = null;
            List<string> ListFiles = new List<string>();
            Pee pee = new Pee();
            string PathDoc = Path.Combine(_env.ContentRootPath);
            if (PeecheckBox.Length != 0)
            {

                for (int i = 0; i < PeecheckBox.Length; i++)
                {

                    value = PeecheckBox[i];
                    pee = PrintWord.GetDataBeneficiairePeeById(value);
                    ListFiles.AddRange(PrintWord.GetPathFile(value, id));
                }
            }
                if (ListFiles.Count() > 1)
                {
                    using (var ZipDoc = new ZipFile())
                    {
                        FileNameZip = $"{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}_{pee.MatriculeBeneficiaireNavigation.PrenomBeneficiaire}_{value}";
                        for (int j = 0; j < ListFiles.Count(); j++)
                        {
                            string nomFichier = $"{pee.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire}-{pee.MatriculeBeneficiaireNavigation.NomBeneficiaire}-{pee.IdEtablissement}-{pee.IdOffreFormation}_{value}";
                            ContentDisposition content = new ContentDisposition()
                            {
                                FileName = $"{nomFichier}.docx",

                                Inline = false
                            };
                            Response.Headers.Add($"Content-Disposition-{j}_{value}", content.ToString());
                            Response.Headers.Add($"X-Content-Type-Options-{j}_{value}", "nosniff");
                        string doc = Path.GetFileName(ListFiles[j]);
                        string regexPath = Path.GetFileNameWithoutExtension(Regex.Replace(doc, @"[0-9,-]", ""));
                        doc = $"{regexPath}_{nomFichier}.docx";
                            System.IO.File.Copy(ListFiles[j], doc);
                            ZipDoc.AddFile(doc);
                            System.IO.File.Delete(ListFiles[j]);

                        }
                        ZipDoc.Save(outPutStream);
                        Directory.GetFiles(PathDoc, "*.docx", SearchOption.TopDirectoryOnly).ToList().ForEach(System.IO.File.Delete);

                    }
                    outPutStream.Position = 0;
                    var fileByte = File(outPutStream, "application/zip", $"{FileNameZip}.zip");
                    return fileByte;
                }

                else 
                {
                    for (int i = 0; i < PeecheckBox.Length; i++) { value = PeecheckBox[i]; }
                    pee = PrintWord.GetDataBeneficiairePeeById(value);                   
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
        //public IActionResult PrintPdf()
        //{
        //    var pintPdf = new ActionAsPdf("GetDocumentForPrint");
        //    return pintPdf;
        //}
        #endregion
        [HttpGet]
        public  IActionResult ListePeeAValider(string id)
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

        


    }
}
