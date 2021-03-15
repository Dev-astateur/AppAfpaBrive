using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layer;
using AppAfpaBrive.BOL;
using System.Diagnostics;
using AppAfpaBrive.Web.Models;
using System.Web.Providers.Entities;

namespace AppAfpaBrive.Web.Controllers.Convention
{
    public class ConventionController : Controller
    {
        private Layer_Offres_Formation _beneficiaireOffre = null;
        private Layer_Etablissement _Etablissement = null;
        private Layer_Code_Produit_Formation _Produit_Formation = null;
        private Layer_Entreprise _Entreprise = null;
        public ConventionController (AFPANADbContext context)
        {
            _beneficiaireOffre = new Layer_Offres_Formation(context);
            _Etablissement = new Layer_Etablissement(context);
            _Produit_Formation = new Layer_Code_Produit_Formation(context);
            _Entreprise = new Layer_Entreprise(context);
        }

        public IActionResult Index()
        {
            IEnumerable<BeneficiaireOffreFormation> beneficiaires = _beneficiaireOffre.GetFormations("azerty12");
            List<Creation_convention> obj = new List<Creation_convention>();
            
            foreach (var item in beneficiaires)
            {
                Creation_convention convention = new Creation_convention
                {
                    IdFormation = item.IdOffreFormation,
                    Idmatricule = item.MatriculeBeneficiaire,
                    IdEtablissement = item.Idetablissement,
                    Etablissement = _Etablissement.Get_Etablissement_Nom(item.Idetablissement).FirstOrDefault().NomEtablissement,
                    Formation = _Produit_Formation.Get_Formation_Nom(item.IdOffreFormation).FirstOrDefault()
                };
                obj.Add(convention);
            }
            return View(obj);
        }

        // get Entreprise
        public IActionResult Entreprise(int? id)
        {
            BeneficiaireOffreFormation beneficiaire = _beneficiaireOffre.GetFormations("azerty12")
                .Where(x=>x.IdOffreFormation == id).FirstOrDefault();
            ViewBag.beneficiaire = beneficiaire;
            return View();
        }

        // post Entreprise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entreprise(Entreprise obj)
        {
            if (ModelState.IsValid)
            {
                var entreprise = _Entreprise.get_Formation(obj.NumeroSiret).FirstOrDefault();
                if(entreprise is null)
                {
                    return View(obj);
                }
                return RedirectToAction("index");
            }
            return View(obj);
        }

    }
}
