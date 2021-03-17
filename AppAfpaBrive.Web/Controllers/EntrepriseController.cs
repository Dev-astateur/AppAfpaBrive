using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class EntrepriseController : Controller
    {
        private readonly AFPANADbContext _dbContext;
        private readonly EntrepriseLayer _layer;

        public EntrepriseController(AFPANADbContext Db)
        {
           
            _layer = new EntrepriseLayer(Db);
            _dbContext = new AFPANADbContext();
        }

        // GET: EntrepriseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EntrepriseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntrepriseController/Create
        public ActionResult Create()
        {
            return View();
        }
       
        // GET: EntrepriseController/ListeEntreprise
        //[HttpGet]
        public async Task<IActionResult> ListeEntreprise(string departement, string formation)
        {
           List<EntrepriseListViewModel> ListentrepriseListViewModel = new List<EntrepriseListViewModel>();
           
            ViewData["GetDepartement"] = departement;
            ViewData["GetProduitForm"] = formation;
            var query = _layer.GetAllEntreprise();
            //if (!String.IsNullOrEmpty(departement))
            //{
            //    query = _layer.GetEntreprisesByDepartement(departement);
            //}

            if (!String.IsNullOrEmpty(departement)&& (!String.IsNullOrEmpty(formation)))
            {
                query =_layer.GetEntrepriseByDepartementEtOffre(formation, departement);
            }


            else if (!String.IsNullOrEmpty(departement)&& String.IsNullOrEmpty(formation))
            {
                query = _layer.GetEntreprisesByDepartement(departement);
            }


            else if (!String.IsNullOrEmpty(formation)&& (String.IsNullOrEmpty(departement)))
            {
                query = _layer.GetEntrepriseByProduitFormation(formation);
            }
            foreach (var entreprise in query)
            {
                EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel();
                entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
                entrepriseModel.Ville = entreprise.Ville;
                entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
                entrepriseModel.MailEntreprise = entreprise.MailEntreprise;
               
                ListentrepriseListViewModel.Add(entrepriseModel);
            }
           
           
            return View(ListentrepriseListViewModel);
        }


        public async Task<IActionResult> ListeEntreprisePourModification(string departement, string formation)
        {
            List<EntrepriseListViewModel> ListentrepriseListViewModel = new List<EntrepriseListViewModel>();

            ViewData["GetDepartement"] = departement;
            ViewData["GetProduitForm"] = formation;
            var query = _layer.GetAllEntreprise();
            //if (!String.IsNullOrEmpty(departement))
            //{
            //    query = _layer.GetEntreprisesByDepartement(departement);
            //}

            if (!String.IsNullOrEmpty(departement) && (!String.IsNullOrEmpty(formation)))
            {
                query = _layer.GetEntrepriseByDepartementEtOffre(formation, departement);
            }


            else if (!String.IsNullOrEmpty(departement) && String.IsNullOrEmpty(formation))
            {
                query = _layer.GetEntreprisesByDepartement(departement);
            }


            else if (!String.IsNullOrEmpty(formation) && (String.IsNullOrEmpty(departement)))
            {
                query = _layer.GetEntrepriseByProduitFormation(formation);
            }
            foreach (var entreprise in query)
            {
                EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel();
                entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
                entrepriseModel.Ville = entreprise.Ville;
                entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
                entrepriseModel.MailEntreprise = entreprise.MailEntreprise;
                entrepriseModel.NumeroSiret = entreprise.NumeroSiret;
                entrepriseModel.IdEntreprise = entreprise.IdEntreprise;
                entrepriseModel.Ligne1Adresse = entreprise.Ligne1Adresse;
                ListentrepriseListViewModel.Add(entrepriseModel);
            }


            return View(ListentrepriseListViewModel);
        }

        // GET: EntrepriseController/creerEntreprise
        [HttpGet]
        public ActionResult CreerEntreprise()
        {
            //List<Pays> Liste = _layer.GetAllPays().ToList();
            // ViewBag.MaList = Liste;
            return View();
        }
      
            

        // POST: EntrepriseController/creerEntreprise
        [HttpPost]
        public ActionResult CreerEntreprise(Entreprise entreprise)
        {

            try
            {
                _dbContext.Entreprises.Add(entreprise);
                _dbContext.SaveChanges();
                return RedirectToAction("ListeEntreprise", "Entreprise");
            }
            catch
            {
                return View();
            }
           
        }
        [HttpGet]
        public ActionResult SuppressionEntreprise(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var query = _layer.GetEntrepriseById(id);
            if (query == null)
            {
                return NotFound();
            }
            return View(query);      
        }
        public ActionResult SupprimerEntreprise (int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _layer.RemoveEntrepriseById(id);

            return RedirectToAction("ListeEntreprisePourModification");

        }

        // POST: EntrepriseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EntrepriseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntrepriseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EntrepriseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntrepriseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
