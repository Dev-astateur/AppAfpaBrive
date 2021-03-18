using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;

using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppAfpaBrive.Web.Layers;
using ReflectionIT.Mvc.Paging;

namespace AppAfpaBrive.Web.Controllers
{
    public class EntrepriseController : Controller
    {
        //private readonly AFPANADbContext _dbContext;
        private readonly EntrepriseLayer _layer;

        public EntrepriseController(AFPANADbContext Db)
        {
            _layer = new EntrepriseLayer(Db);
            //_dbContext = new AFPANADbContext();
        }

       


        #region ModifierEntreprise
        // GET: EntrepriseController


        [HttpGet]
        public ActionResult ModifierEntreprise(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            var entreprise = _layer.GetEntrepriseById(id);
            if (entreprise == null)
            {
                return NotFound();
            }
            EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel(entreprise);

            //EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel();
            //entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
            //entrepriseModel.Ville = entreprise.Ville;
            //entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
            //entrepriseModel.MailEntreprise = entreprise.MailEntreprise;
            //entrepriseModel.NumeroSiret = entreprise.NumeroSiret;
            //entrepriseModel.IdEntreprise = entreprise.IdEntreprise;
            //entrepriseModel.Ligne1Adresse = entreprise.Ligne1Adresse;
            return View(entrepriseModel);
        }
        [HttpPost]
        public ActionResult ModifierEntreprise(Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                //_dbContext.Entry(entreprise).State = EntityState.Modified;
                //_dbContext.SaveChanges();
                _layer.ModifierEntreprise(entreprise);
                return RedirectToAction("ListeEntreprisePourModification");
            }

            return View();
            


        }
        #endregion


        #region ListeEntreprise
        // GET: EntrepriseController/ListeEntreprise
        //[HttpGet]


        public async Task<IActionResult> ListeEntreprise(string departement, string formation, int page)
        {

            List<EntrepriseListViewModel> ListentrepriseListViewModel = new List<EntrepriseListViewModel>();

            ViewData["GetDepartement"] = departement;
            // ViewData["GetProduitForm"] = formation;

            //var query = _layer.GetAllEntreprise();

            List<Entreprise> query=null;



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

            if (query!=null)
            {
                foreach (var entreprise in query)
                {
                    EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel(entreprise);
                    //entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
                    //entrepriseModel.Ville = entreprise.Ville;
                    //entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
                    //entrepriseModel.MailEntreprise = entreprise.MailEntreprise;

                    ListentrepriseListViewModel.Add(entrepriseModel);
                    
                }
                ;
                return View(ListentrepriseListViewModel);
            }

            //Test pour paging
            var qry = _layer.GetAllEntreprise();
            var model = PagingList.CreateAsync(qry, 10, page);
            return View();

            
        }
        #endregion


        #region ListeEntrepriseModification
        public async Task<IActionResult> ListeEntreprisePourModification(string departement, string formation)
        {
            List<EntrepriseListViewModel> ListentrepriseListViewModel = new List<EntrepriseListViewModel>();

            ViewData["GetDepartement"] = departement;
            //ViewData["GetProduitForm"] = formation;
            // var query = _layer.GetAllEntreprise();
            List<Entreprise> query=null;

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
            if (query!=null)
            {
                foreach (var entreprise in query)
                {
                    EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel(entreprise);
                    //entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
                    //entrepriseModel.Ville = entreprise.Ville;
                    //entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
                    //entrepriseModel.MailEntreprise = entreprise.MailEntreprise;
                    //entrepriseModel.NumeroSiret = entreprise.NumeroSiret;
                    //entrepriseModel.IdEntreprise = entreprise.IdEntreprise;
                    //entrepriseModel.Ligne1Adresse = entreprise.Ligne1Adresse;
                    ListentrepriseListViewModel.Add(entrepriseModel);
                }
                return View(ListentrepriseListViewModel);
            }

            return View();

            
        }
        #endregion


        #region CreerEntreprise
        // GET: EntrepriseController/creerEntreprise
        [HttpGet]
        public ActionResult CreerEntreprise()
        {
           
            return View();
        }

        
        // POST: EntrepriseController/creerEntreprise
        [HttpPost]
        public ActionResult CreerEntreprise(Entreprise entreprise)
        {
            

                string libellePays = entreprise.Idpays2Navigation.LibellePays;
              
            entreprise.Idpays2= _layer.GetIdPaysByMatriculePays(libellePays);
            try
            {
                
                _layer.AddEntreprise(entreprise);
                //_dbContext.Entreprises.Add(entreprise);
                //_dbContext.SaveChanges();
                return RedirectToAction("ListeEntreprisePourModification", "Entreprise");
            }
            catch
            {
                return View();
            }
        }
        #endregion


        #region SuppressionEntreprise
        [HttpGet]
        public ActionResult SuppressionEntreprise(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var entreprise = _layer.GetEntrepriseById(id);
            if (entreprise == null)
            {
                return NotFound();
            }
            EntrepriseListViewModel entrepriseModel = new EntrepriseListViewModel(entreprise);
            //entrepriseModel.RaisonSociale = entreprise.RaisonSociale;
            //entrepriseModel.Ville = entreprise.Ville;
            //entrepriseModel.TelEntreprise = entreprise.TelEntreprise;
            //entrepriseModel.MailEntreprise = entreprise.MailEntreprise;
            //entrepriseModel.NumeroSiret = entreprise.NumeroSiret;
            //entrepriseModel.IdEntreprise = entreprise.IdEntreprise;
            //entrepriseModel.Ligne1Adresse = entreprise.Ligne1Adresse;

            return View(entrepriseModel);
        }


        public ActionResult SupprimerEntreprise(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _layer.RemoveEntrepriseById(id);

            return RedirectToAction("ListeEntreprisePourModification");

        }
        #endregion


      

        // GET: EntrepriseController/Edit/5
        public ActionResult Details(int id)
        {
            Entreprise entreprise = _layer.GetEntrepriseById(id);
            EntrepriseListViewModel entrepriseListViewModel = new EntrepriseListViewModel(entreprise);
            
            return View(entrepriseListViewModel);
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
