﻿using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.Web.ModelView;

namespace AppAfpaBrive.Web.Controllers.Formateur
{
    public class OffreDeFormationBeneficiaireController : Controller
    {
        private OffreDeFormationLayer _offreDeFormationLayer;
        private BeneficiaireOffreFormationLayer _beneficiaireOffreFormationLayer;


        public OffreDeFormationBeneficiaireController(AFPANADbContext context)
        {
            _offreDeFormationLayer = new OffreDeFormationLayer(context);
            _beneficiaireOffreFormationLayer = new BeneficiaireOffreFormationLayer(context);

        }
        // GET: StagiaireParOffredeFormationController
        //[HttpPost]
        public ActionResult OffreDeFormationBeneficiaire()
        {
            
            this.ViewBag.MonTitre = "OffreDeFormationBeneficiaire";

            var query = _offreDeFormationLayer.GetByMatriculeCollaborateurAFPA("96GB011");
           
            OffreFormationSpecifiqueModelView model = new OffreFormationSpecifiqueModelView(query);
            int selectedvalue = model.IdOffreFormation;
            query.BeneficiaireOffreFormations = _beneficiaireOffreFormationLayer.GetAllByOffreFormation(selectedvalue);
            model.AlimenterListeOffreFormations(_offreDeFormationLayer.GetAllbyMatricule("96GB011"));
            


            foreach (var item in query.BeneficiaireOffreFormations)
            {
                model.BeneficiaireOffreFormations.Add(item);
            }
            var selectedValue = model.IdOffreFormation;
            
            return View(model);
        }
        // GET: OffreDeFormationBeneficiaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OffreDeFormationBeneficiaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OffreDeFormationBeneficiaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Create
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

        // GET: OffreDeFormationBeneficiaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Edit/5
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

        // GET: OffreDeFormationBeneficiaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OffreDeFormationBeneficiaireController/Delete/5
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
