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
using DocumentFormat.OpenXml;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.Layers;

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
        private PeeLayer _peelayer = null;
        private Periode_pee_Layer _periode = null;
        public ConventionController(AFPANADbContext context)
        {
            _beneficiaireOffre = new Layer_Offres_Formation(context);
            _Etablissement = new Layer_Etablissement(context);
            _Produit_Formation = new Layer_Code_Produit_Formation(context);
            _Entreprise = new Layer_Entreprise(context);
            _Pays = new Layer_Pays(context);
            _pro = new Layer_Professionnel(context);
            _entreprisepro = new Layer_EntrepriseProfessionnel(context);
            _peelayer = new PeeLayer(context);
            _periode = new Periode_pee_Layer(context);
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

            if (id == 0)
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
                if (entreprise is null)
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
            List<Professionnel> pro = _pro.Get_Pro(convention.IdEntreprise);
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
            return RedirectToAction("date");
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
            if (ModelState.IsValid)
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
            string str = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> date = new List<Date_ModelView>();
            if (str != null)
            {
                date = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            }
            return View(date);
        }

        // get post
        [HttpPost]
        public IActionResult date(Date_ModelView date)
        {
            return View(date);
        }

        // get date
        public IActionResult date_create()
        {

            return View();
        }

        //  post
        [HttpPost]
        public IActionResult date_create(Date_ModelView date)
        {
            List<Date_ModelView> listDate = new List<Date_ModelView>();
            string str = this.HttpContext.Session.GetString("date");
            if (str != null)
            {
                listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            }
            date.Iddate = listDate.Count() + 1;
            listDate.Add(date);
            str = JsonConvert.SerializeObject(listDate);
            HttpContext.Session.SetString("date", str);
            return RedirectToAction("date");


            //return View(date);
        }

        // get date
        public IActionResult date_delete()
        {
            return View();
        }

        // get post
        [HttpPost]
        public IActionResult date_delete(Date_ModelView date)
        {
            return View();
        }

        // get recapitulatif
        public IActionResult Recapitulatif()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

            string str_date = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> dates = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date);
            ViewBag.dates = dates;

            return View(convention);
        }

        //post Recapitulatif
        [HttpPost]
        public IActionResult Recapitulatif(Creation_convention oui)
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            Pee pee = new Pee
            {
                IdEntreprise = convention.IdEntreprise,
                MatriculeBeneficiaire = convention.Idmatricule,
                IdTuteur = convention.IdTuteur,
                IdResponsableJuridique = convention.IdResponsable,
                IdOffreFormation = convention.IdFormation,
                IdEtablissement = convention.IdEtablissement
            };

            string str_date = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> dates = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date);
            _peelayer.Pee_Create(pee);
            decimal id = _peelayer.GetPeeBy_Idmatricule_idFormation_idetablissemnt(pee.MatriculeBeneficiaire, pee.IdEntreprise, pee.IdEtablissement);
            foreach (var item in dates)
            {
                PeriodePee periodePee = new PeriodePee
                {
                    IdPee = id,
                    DateDebutPeriodePee = item.Date1,
                    DateFinPeriodePee = item.Date2,
                    NumOrdre = item.Iddate
                };
                _periode.Pee_Create(periodePee);
            }


            return RedirectToAction("index");
        }
    }
}
