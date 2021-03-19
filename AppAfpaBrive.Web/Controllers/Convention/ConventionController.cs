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
        private Layer_EntrepriseProfessionnel _entreprisepro = null;
        public ConventionController (AFPANADbContext context)
        {
            _beneficiaireOffre = new Layer_Offres_Formation(context);
            _Etablissement = new Layer_Etablissement(context);
            _Produit_Formation = new Layer_Code_Produit_Formation(context);
            _Entreprise = new Layer_Entreprise(context);
            _Pays = new Layer_Pays(context);
            _pro = new Layer_Professionnel(context);
            _entreprisepro = new Layer_EntrepriseProfessionnel(context);
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
            convention.Raison_social = obj.RaisonSociale;
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

                Entreprise entreprise1 = new Entreprise
                {
                    CodePostal = entreprise.CodePostal,
                    Ligne1Adresse = entreprise.Ligne1Adresse,
                    Ligne2Adresse = entreprise.Ligne2Adresse,
                    Ligne3Adresse = entreprise.Ligne3Adresse,
                    RaisonSociale = entreprise.RaisonSociale,
                    NumeroSiret = entreprise.NumeroSiret,
                    Ville = entreprise.Ville,
                    Idpays2 = _Pays.Get_pays_ID(entreprise.Idpays2)
                };
                _Entreprise.Create_entreprise(entreprise1);

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
            List<Professionnel> pro =_pro.Get_Pro(convention.IdEntreprise);
            ViewBag.pro = pro;
            return View(pro);
        }

        //post Professionel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Professionel(List<Professionnel> obj)
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            if (Request.Form["tuteur"] != "")
            {
                string tuteurID = Request.Form["tuteur"].ToString();
                Professionnel professionnel = new Professionnel(); 
                professionnel = _pro.GetProfessionnel(int.Parse(tuteurID));
                convention.TuteurNom = professionnel.NomProfessionnel;
                convention.TuteurPrenom = professionnel.PrenomProfessionnel;
                convention.IdTuteur = professionnel.IdProfessionnel;
            }
            if (Request.Form["Responsable"] != "")
            {
                string ResponsableID = Request.Form["Responsable"].ToString();
                Professionnel professionnel = new Professionnel();
                professionnel = _pro.GetProfessionnel(int.Parse(ResponsableID));
                convention.ResponsableNom = professionnel.NomProfessionnel;
                convention.ResponsablePrenom = professionnel.PrenomProfessionnel;
                convention.IdResponsable = professionnel.IdProfessionnel;
            }
            var x = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", x);
            return RedirectToAction("Recapitulatif");
        }

        // get Professionel_creation
        public IActionResult Recapitulatif()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            return View(convention);
        }

        // get Professionel_creation
        public IActionResult Professionel_Creation()
        {
            
            return View();
        }

        // post Professionel_Creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Professionel_Creation(Professionnel_ModelView obj)
        {
            if(ModelState.IsValid)
            {
                string str = this.HttpContext.Session.GetString("convention");
                Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
                Professionnel pro = new Professionnel
                {
                    CodeTitreCiviliteProfessionnel = obj.CodeTitreCiviliteProfessionnel,
                    NomProfessionnel = obj.NomProfessionnel,
                    PrenomProfessionnel = obj.PrenomProfessionnel
                };
                _pro.create(pro);
                EntrepriseProfessionnel entrepriseProfessionnel = new EntrepriseProfessionnel
                {
                    IdProfessionnel = _pro.Get_Id_pro(pro.NomProfessionnel, pro.PrenomProfessionnel),
                    AdresseMailPro = obj.AdresseMail,
                    TelephonePro = obj.NumerosTel,
                    IdEntreprise = convention.IdEntreprise
                };
                _entreprisepro.create(entrepriseProfessionnel);
                return RedirectToAction("Professionel");
            }

            return View(obj);
        }
        // get date
        public IActionResult date()
        {

            return View();
        }

        // get post
        public IActionResult date(string id)
        {
            string Date1 = Request.Form["date1"];
            string Date2 = Request.Form["date2"];
            string Date3 = Request.Form["date3"];
            string Date4 = Request.Form["date4"];

            return View();
        }
    }
}
