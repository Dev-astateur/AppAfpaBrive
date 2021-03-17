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
using AppAfpaBrive.Web.ModelView;


namespace AppAfpaBrive.Web.Controllers.Convention
{
    public class ConventionController : Controller
    {
        private Layer_Offres_Formation _beneficiaireOffre = null;
        private Layer_Etablissement _Etablissement = null;
        private Layer_Code_Produit_Formation _Produit_Formation = null;
        private Layer_Entreprise _Entreprise = null;
        private Layer_Pays _Pays = null;
        private Layer_Professionnel _pro = null;
        public ConventionController (AFPANADbContext context)
        {
            _beneficiaireOffre = new Layer_Offres_Formation(context);
            _Etablissement = new Layer_Etablissement(context);
            _Produit_Formation = new Layer_Code_Produit_Formation(context);
            _Entreprise = new Layer_Entreprise(context);
            _Pays = new Layer_Pays(context);
            _pro = new Layer_Professionnel(context);
        }

        // get index
        public IActionResult Index()
        {
            IEnumerable<BeneficiaireOffreFormation> beneficiaires = _beneficiaireOffre.GetFormations("16174318");
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
            Creation_convention Session_Convention = new Creation_convention
            {
                Idmatricule = "16174318"
            };
            var str = JsonConvert.SerializeObject(Session_Convention);
            HttpContext.Session.SetString("convention", str);
            return View(obj);
        }

        // post index
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Index(Creation_convention convention)
        //{
        //    var str = JsonConvert.SerializeObject(convention);
        //    HttpContext.Session.SetString("convention", str);
        //    return RedirectToAction("Entreprise");
        //}

        // get Entreprise
        public IActionResult Entreprise(int id)
        {
            string str = HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

            if(id ==0)
            {
                id = convention.IdFormation;
            }

            convention.Formation = _Produit_Formation.Get_Formation_Nom(id).FirstOrDefault();
            convention.IdFormation = id;
            convention.IdEtablissement = _beneficiaireOffre.GetIdetablissemnt(convention.Idmatricule, id).FirstOrDefault().Idetablissement;
            convention.Etablissement = _Etablissement.Get_Etablissement_Nom(convention.IdEtablissement).FirstOrDefault().NomEtablissement;
            var obj = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", obj);
            return View();
        }

        // post Entreprise
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entreprise(Entreprise_Siret obj)
        {
            if (ModelState.IsValid)
            {
                var entreprise = _Entreprise.get_Entreprise(obj.NumeroSiret).FirstOrDefault();
                if(entreprise is null)
                {
                    return RedirectToAction("Entreprise_creation");
                }
                string str = this.HttpContext.Session.GetString("convention");
                Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
                convention.Siret = obj.NumeroSiret;
                str = JsonConvert.SerializeObject(convention);
                HttpContext.Session.SetString("convention", str);
                return RedirectToAction("Entreprise_Recap");
            }
            return View(obj);
        }


        // get Entreprise_Recap
        public IActionResult Entreprise_Recap()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

            Entreprise obj = _Entreprise.get_Entreprise(convention.Siret).FirstOrDefault();
            convention.Siret = obj.NumeroSiret;
            convention.IdEntreprise = obj.IdEntreprise;

            str = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", str); 
            return View(obj);
        }

        // get Entreprise_creation
        public IActionResult Entreprise_creation()
        {
            IQueryable<string> pays = _Pays.Get_pays();
            ViewBag.pays = pays;
            return View();
        }


        // post Entreprise_creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Entreprise_creation(Entreprise_Creation_ViewModel entreprise)
        {
            if (ModelState.IsValid)
            {
                string str = this.HttpContext.Session.GetString("convention");
                Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

                //_Entreprise.Create_entreprise(entreprise);

                convention.Siret = entreprise.NumeroSiret;
                str = JsonConvert.SerializeObject(convention);
                HttpContext.Session.SetString("convention", str);

                return RedirectToAction("Entreprise_Recap");
            }
            return View(entreprise);
        }

        // get Professionel
        public IActionResult Professionel()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            var pro = _pro.Get_Pro(convention.IdEntreprise);
            ViewBag.professionel = pro;
            return View();
        }
    }
}
