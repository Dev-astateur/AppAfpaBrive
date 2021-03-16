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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

        // get index
        public IActionResult Index()
        {
            this.HttpContext.Session.SetString("matricule", "azerty12");
            IEnumerable<BeneficiaireOffreFormation> beneficiaires = _beneficiaireOffre.GetFormations(this.HttpContext.Session.GetString("matricule"));
            List<Creation_convention> obj = new List<Creation_convention>();
            
            foreach (var item in beneficiaires)
            {
                Creation_convention convention = new Creation_convention
                {
                    IdFormation = item.IdOffreFormation,
                    Idmatricule = item.MatriculeBeneficiaire,
                    IdEtablissement = item.Idetablissement,
                    DateDebut = item.DateEntreeBeneficiaire,
                    Datefin = item.DateSortieBeneficiaire,
                    Etablissement = _Etablissement.Get_Etablissement_Nom(item.Idetablissement).FirstOrDefault().NomEtablissement,
                    Formation = _Produit_Formation.Get_Formation_Nom(item.IdOffreFormation).FirstOrDefault()
                };
                obj.Add(convention);
            }
            return View(obj);
        }

        // post index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Creation_convention convention)
        {
            var str = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", str);
            return RedirectToAction("Entreprise");
        }

        // get Entreprise
        public IActionResult Entreprise(int? id)
        {
            var str = HttpContext.Session.GetString("convention");
            var obj = JsonConvert.DeserializeObject<Creation_convention>(str);
            return View();
        }

        // post Entreprise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entreprise(Entreprise obj)
        {
            if (ModelState.IsValid)
            {
                var entreprise = _Entreprise.get_Entreprise(obj.NumeroSiret).FirstOrDefault();
                if(entreprise is null)
                {
                    return View(obj);
                }
                this.HttpContext.Session.SetString("Siret", obj.NumeroSiret);
                return RedirectToAction("Entreprise_Recap");
            }
            return View(obj);
        }


        // get Entreprise_Recap
        public IActionResult Entreprise_Recap()
        {
            
            Entreprise obj = _Entreprise.get_Entreprise(this.HttpContext.Session.GetString("Siret"))
                .FirstOrDefault();
            return View(obj);
        }

        // get Entreprise_creation
        public IActionResult Entreprise_creation()
        {

            return View();
        }


        // post Entreprise_creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entreprise_creation(Entreprise entreprise)
        {
            if (ModelState.IsValid)
            {
                if (entreprise is null)
                {
                    return View(entreprise);
                }
                return RedirectToAction("Entreprise_Recap");
            }
            return View(entreprise);
        }
    }
}
