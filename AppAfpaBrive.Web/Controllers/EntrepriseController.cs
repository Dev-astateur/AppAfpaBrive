using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

using AppAfpaBrive.Web.Layers;
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
        //public ActionResult ListeEntreprise()       
        //{
        //    //  List<Entreprise> ListEntreprise = _dbContext.Entreprises.ToList();
        //    IEnumerable<Entreprise> ListEntreprise = _layer.GetAllEntreprise();
        //    return View(ListEntreprise.ToList());
        //}
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


        // GET: EntrepriseController/creerEntreprise
        [HttpGet]
        public ActionResult CreerEntreprise()
        {
            //List<Pay> Liste = _dbContext.Pays.ToList();
            // ViewBag.MaList = Liste;
            return View();
        }
      
            

        // POST: EntrepriseController/creerEntreprise
        [HttpPost]
        public ActionResult CreerEntreprise(Entreprise entreprise)
        {

            _dbContext.Entreprises.Add(entreprise);
            _dbContext.SaveChanges();
            return View();
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
