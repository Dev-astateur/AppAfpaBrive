using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Areas.Identity.Data;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.Web.ModelView;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace AppAfpaBrive.Web.Controllers.CollaborateurAfpa
{
    public class CollaborateurAfpaController : Controller
    {
        private readonly AFPANADbContext _db;
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IFileProvider _fileProvider;

        public CollaborateurAfpaController(AFPANADbContext db, UserManager<AppAfpaBriveUser> userManager, IConfiguration config, IFileProvider fileProvider)
        {
            _db = db;
            _userManager = userManager;
            _config = config;
            _fileProvider = fileProvider;

        }
        // GET: CollaborateurAfpaController
        public async Task<IActionResult> Index(string filter, int pageIndex, string sortExpression = "NomCollaborateur")
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);

            var model = await _collaborateurLayer.GetPage(filter, pageIndex, sortExpression);
            model.Action = "Index";
            model.RouteValue = new RouteValueDictionary
            {
                {"filter", filter }
            };
            return View(model);
        }

        // GET: CollaborateurAfpaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollaborateurAfpaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollaborateurAfpaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CollaborateurAfpaModelView obj)
        {
            var x = Request.Form["TitreCivilite"].ToString();
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            var check = _collaborateurLayer.CheckMatriculeCollaborateurExiste(obj.MatriculeCollaborateurAfpa);
            if (check)
            {
                ModelState.AddModelError("MatriculeCollaborateurAfpa", "Ce Matricule Collaborateur existe deja");
                return View();
            }
            if (ModelState.IsValid)
            {
                if (x == "0")
                {
                    obj.CodeTitreCivilite = 0;
                }
                else obj.CodeTitreCivilite = 1;
                _collaborateurLayer.InsertProduit(obj.GetCollaborateur());


                return RedirectToAction("Index");
            }
            return this.View(obj);
        }
        /// <summary>
        /// Ajout compte utilisateur si création
        /// </summary>
        /// <param name="beneficiaire"></param>
        private async void AjouterCompteUtilisateur(BOL.CollaborateurAfpa collaborateur)
        {
            var user = await _userManager.FindByNameAsync(collaborateur.MatriculeCollaborateurAfpa);
            if (user == null)
            {
                user = new AppAfpaBriveUser
                {
                    UserName = collaborateur.MatriculeCollaborateurAfpa,
                    Email = collaborateur.MailCollaborateurAfpa,
                    EmailConfirmed = true,
                    MotPasseAChanger = true,
                    Nom = collaborateur.NomCollaborateur,
                    Prenom = collaborateur.PrenomCollaborateur,
                    Theme = "cyborg"
                };

                Task<IdentityResult> userResult = _userManager.CreateAsync(user, $"Afpa{collaborateur.MatriculeCollaborateurAfpa}!");
                userResult.Wait();
                if (userResult.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(user, "CollaborateurAFPA");
                    newUserRole.Wait();
                }
            }

        }
        // GET: CollaborateurAfpaController/Delete/5
        public IActionResult IntegrationExcel()
        {
            string path = _fileProvider.GetFileInfo(_config.GetSection("RepertoireImportData").Value).PhysicalPath;
            string filePath = @$"{path}/ListeSalaries.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                if (reader.Read()) // Lecture première ligne Entêtes
                {
                    TraiterDonnees(reader);
                    return Ok();
                }
                // Fichier vide Exception
                else
                {
                    throw new ApplicationException("Fichier Vide. Extraction abandonnée");
                }


            }
        }
        /// <summary>
        /// Les données sont intégrées dans la base de données 
        /// sous forme d'un seul lot transactionnel
        /// Ajout du compte utilisateur associé (collaborateur AFPA ou Formateur)
        /// Définition du rôle dans l'application
        /// Ajout des UO inexistantes et assignation au collaborateur
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="collabLayer"></param>
        private void TraiterDonnees(IExcelDataReader reader)
        {

            while (reader.Read())
            {
                if (_db.CollaborateurAfpas.Find(reader.GetValue(0).ToString()) == null)
                {

                    BOL.CollaborateurAfpa collaborateurAfpa = new();


                    collaborateurAfpa.MatriculeCollaborateurAfpa = reader.GetValue(0).ToString();
                    collaborateurAfpa.IdEtablissement = (string.IsNullOrEmpty(reader.GetValue(6).ToString())) ? null : reader.GetValue(6).ToString();
                    collaborateurAfpa.CodeTitreCivilite = (reader.GetValue(3).ToString() == "1") ? 0 : 1;
                    collaborateurAfpa.NomCollaborateur = reader.GetString(1);
                    collaborateurAfpa.PrenomCollaborateur = reader.GetString(2);
                    collaborateurAfpa.TelCollaborateurAfpa = (string.IsNullOrEmpty(reader.GetString(4))) ? null : reader.GetString(4);
                    collaborateurAfpa.MailCollaborateurAfpa = (string.IsNullOrEmpty(reader.GetString(5))) ? null : reader.GetString(5);
                    collaborateurAfpa.Uo = (reader.GetValue(9)==null) ? null : reader.GetValue(9).ToString();
                    collaborateurAfpa.UserId = collaborateurAfpa.MatriculeCollaborateurAfpa;
                    
                    if (collaborateurAfpa.Uo != null && _db.UniteOrganisationnelles.Find(collaborateurAfpa.Uo) == null)
                    {
                        _db.UniteOrganisationnelles.Add(new UniteOrganisationnelle { LibelleUo = reader.GetString(7), Uo = reader.GetValue(9).ToString() });
                    }
                    _db.CollaborateurAfpas.Add(collaborateurAfpa);

                    // Ajout du compte pour authentification
                    Task<AppAfpaBriveUser> userT = _userManager.FindByNameAsync(collaborateurAfpa.MatriculeCollaborateurAfpa);
                    userT.Wait();
                    // Choix du rôle : fonction contient formateur (Formateur) ou non (collaborateurAfpa)... 
                    string fonction = reader.GetString(8).ToUpper();
                    string role;
                    if (fonction.Contains("FORMATEUR"))
                    {
                        role = "Formateur";
                    }
                    else
                    {
                        role = "CollaborateurAFPA";
                    }

                    if (userT.Result == null)
                    {
                        var user = new AppAfpaBriveUser
                        {
                            UserName = collaborateurAfpa.MatriculeCollaborateurAfpa,
                            Email = collaborateurAfpa.MailCollaborateurAfpa,
                            EmailConfirmed = true,
                            MotPasseAChanger = true,
                            Nom = collaborateurAfpa.NomCollaborateur,
                            Prenom = collaborateurAfpa.PrenomCollaborateur,

                            Theme = "cyborg"

                        };

                        Task<IdentityResult> userResult = _userManager.CreateAsync(user, $"Afpa{collaborateurAfpa.MatriculeCollaborateurAfpa}!");
                        userResult.Wait();
                        if (userResult.Result.Succeeded)
                        {
                            Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(user, role);
                            newUserRole.Wait();
                        }



                    }
                }

            }
            _db.SaveChanges();
        }
        // GET: CollaborateurAfpaController/Edit/5
        public IActionResult Edit(string id)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (id == null)
            {
                return NotFound();
            }
            CollaborateurAfpaModelView obj = _collaborateurLayer.GetByMatriculeCollaborateur(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: CollaborateurAfpaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CollaborateurAfpaModelView obj)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (ModelState.IsValid)
            {
                _collaborateurLayer.Update(obj.GetCollaborateur());
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: CollaborateurAfpaController/Delete/5
        public IActionResult Delete(string id)
        {
            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            if (id == null)
            {
                return NotFound();
            }
            var obj = _collaborateurLayer.GetByMatriculeCollaborateurDelete(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: CollaborateurAfpaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCollaborateur(string id)
        {

            CollaborateurAfpaLayer _collaborateurLayer = new CollaborateurAfpaLayer(_db);
            CollaborateurAfpaModelView obj = _collaborateurLayer.GetByMatriculeCollaborateur(id);
            if (obj == null)
            {
                return NotFound();
            }
            _collaborateurLayer.Remove(obj.GetCollaborateur());
            return RedirectToAction("Index");
        }
    }
}
