﻿using AppAfpaBrive.BOL;
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
      
        private readonly EntrepriseLayer _layer;

        public EntrepriseController(AFPANADbContext Db)
        {
            _layer = new EntrepriseLayer(Db);
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

            
            return View(entrepriseModel);
        }

       

        [HttpPost]
        public ActionResult ModifierEntreprise(Entreprise entreprise, string Pays)
        {
            if (ModelState.IsValid)
            {
                _layer.ModifierEntreprise(entreprise);
                return RedirectToAction("ListeEntreprisePourModification");
            }

            return View();
            


        }
        #endregion


        #region ListeEntreprise
        // GET: EntrepriseController/ListeEntreprise
        //[HttpGet]


        public ActionResult ListeEntreprise(string departement, string formation, int page)
        {

          

            ViewData["GetDepartement"] = departement;
             ViewData["GetProduitForm"] = formation;

            

            var query = _layer.GetAllEntrepriseForPaging(page);

            if (!String.IsNullOrEmpty(departement) && (!String.IsNullOrEmpty(formation)))
            {
               
                query =  _layer.GetEntrepriseByDepartementEtOffreForPaging(formation,departement,page);

            }

            else if (!String.IsNullOrEmpty(departement) && String.IsNullOrEmpty(formation))
            {
                
                 query =_layer.GetEntreprisesByDepartementPaging(departement, page);
            }

            else if (!String.IsNullOrEmpty(formation) && (String.IsNullOrEmpty(departement)))
            {

                query = _layer.GetEntrepriseByProduitFormationForPaging(formation, page);
            }

            //Test pour paging

            // var qry = _dbContext.Entreprises.OrderBy(e => e.RaisonSociale);
            // var model = PagingList.Create(qry, 10, page);
            // return View(model);
            query.Action = "ListeEntreprise";

            return View(query);         

        }
        #endregion


        #region ListeEntrepriseModification
        public async Task<IActionResult> ListeEntreprisePourModification(string departement, string formation, int page)
        {
          

            ViewData["GetDepartement"] = departement;
            ViewData["GetProduitForm"] = formation;
            var query = _layer.GetAllEntrepriseForPaging(page);

           

            if (!String.IsNullOrEmpty(departement) && (!String.IsNullOrEmpty(formation)))
            {
                query = _layer.GetEntrepriseByDepartementEtOffreForPaging(formation, departement, page);
            }
            else if (!String.IsNullOrEmpty(departement) && String.IsNullOrEmpty(formation))
            {
                query = _layer.GetEntreprisesByDepartementPaging(departement, page);
            }
            else if (!String.IsNullOrEmpty(formation) && (String.IsNullOrEmpty(departement)))
            {
                query = _layer.GetEntrepriseByProduitFormationForPaging(formation, page);
            }
            //Test pour pagination
            //var qry = _dbContext.Entreprises.OrderBy(e => e.RaisonSociale);
            // var model = PagingList.Create(qry, 10, page);
            // model.Action = "ListeEntreprisePourModification";
            // return View(model);


            //If faut assigner une action à la liste pour les pages differentes de la 1 

            query.Action = "ListeEntreprisePourModification";
            return View(query);

            
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
        public ActionResult CreerEntreprise(EntrepriseListViewModel entrepriseVM, string InputPays)
        {
            

            string libellePays = InputPays;

            Entreprise entreprise = new Entreprise();
            entreprise.CodePostal = entrepriseVM.CodePostal;
            entreprise.Ligne1Adresse = entrepriseVM.Ligne1Adresse;
            entreprise.Ligne2Adresse = entrepriseVM.Ligne2Adresse;
            entreprise.Ligne3Adresse = entrepriseVM.Ligne3Adresse;
            entreprise.MailEntreprise = entrepriseVM.MailEntreprise;
            entreprise.NumeroSiret = entrepriseVM.NumeroSiret;
            entreprise.RaisonSociale = entrepriseVM.RaisonSociale;
            entreprise.TelEntreprise = entrepriseVM.TelEntreprise;
            entreprise.Ville = entrepriseVM.Ville;
            entreprise.Idpays2 = _layer.GetIdPaysByMatriculePays(libellePays);

            try
            {
                _layer.AddEntreprise(entreprise);
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
      
    }
}
