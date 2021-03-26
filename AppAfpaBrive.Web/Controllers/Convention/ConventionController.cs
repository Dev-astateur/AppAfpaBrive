using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;
using AppAfpaBrive.BOL;
using System.Diagnostics;
using AppAfpaBrive.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AppAfpaBrive.Web.ModelView;
using DocumentFormat.OpenXml;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.Web.Utilitaires;

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
            IEnumerable<BeneficiaireOffreFormation> beneficiaires = _beneficiaireOffre.GetFormations("20061760");
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
                string str = this.HttpContext.Session.GetString("convention");
                Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
                var entreprise = _Entreprise.get_Entreprise(obj.NumeroSiret).FirstOrDefault();
                if (entreprise is null)
                {
                    convention.Entreprise_Create = true;
                    str = JsonConvert.SerializeObject(convention);
                    HttpContext.Session.SetString("convention", str);
                    HttpContext.Session.SetString("siret", obj.NumeroSiret);
                    return RedirectToAction("Entreprise_creation");
                }
                convention.Entreprise_Create = false;
                convention.Siret = obj.NumeroSiret;
                convention.IdEntreprise = entreprise.IdEntreprise;
                convention.Entreprise_codePostal = entreprise.CodePostal;
                convention.Entreprise_IdPays = entreprise.Idpays2;
                convention.Entreprise_Ligne1Adresse = entreprise.Ligne1Adresse;
                convention.Entreprise_Ligne2Adresse = entreprise.Ligne2Adresse;
                convention.Entreprise_Ligne3Adresse = entreprise.Ligne3Adresse;
                convention.Entreprise_Mail = entreprise.MailEntreprise;
                convention.Entreprise_raison_social = entreprise.RaisonSociale;
                convention.Entreprise_Tel = entreprise.TelEntreprise;
                convention.Entreprise_Ville = entreprise.Ville;

                str = JsonConvert.SerializeObject(convention);
                HttpContext.Session.SetString("convention", str);
                //HttpContext.Session.SetString("pro", "");
                //HttpContext.Session.SetString("date", "");
                return RedirectToAction("Entreprise_Recap");
            }
            return View(obj);
        }


        // get Entreprise_Recap
        public IActionResult Entreprise_Recap()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            //Entreprise obj = _Entreprise.get_Entreprise(convention.Siret).FirstOrDefault();
            //convention.Siret = obj.NumeroSiret;
            //convention.IdEntreprise = obj.IdEntreprise;
            //convention.Entreprise_raison_social = obj.RaisonSociale;

            Entreprise entreprise = new Entreprise
            {
                CodePostal = convention.Entreprise_codePostal,
                Idpays2 = convention.Entreprise_IdPays,
                Ligne1Adresse = convention.Entreprise_Ligne1Adresse,
                Ligne2Adresse = convention.Entreprise_Ligne2Adresse,
                Ligne3Adresse = convention.Entreprise_Ligne3Adresse,
                MailEntreprise = convention.Entreprise_Mail,
                TelEntreprise = convention.Entreprise_Tel,
                NumeroSiret = convention.Siret,
                RaisonSociale = convention.Entreprise_raison_social,
                Ville = convention.Entreprise_Ville
            };
            str = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", str);
            HttpContext.Session.SetString("date", "");
            HttpContext.Session.SetString("pro", "");
            return View(entreprise);
        }

        // get Entreprise_creation
        public IActionResult Entreprise_creation()
        {
            string siret = this.HttpContext.Session.GetString("siret");
            ViewBag.siret = siret;
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
                //Entreprise entreprise1 = new Entreprise
                //{
                //    CodePostal = entreprise.CodePostal,
                //    Ligne1Adresse = entreprise.Ligne1Adresse,
                //    Ligne2Adresse = entreprise.Ligne2Adresse,
                //    Ligne3Adresse = entreprise.Ligne3Adresse,
                //    RaisonSociale = entreprise.RaisonSociale,
                //    NumeroSiret = entreprise.NumeroSiret,
                //    Ville = entreprise.Ville,
                //    Idpays2 = _Pays.Get_pays_ID(entreprise.Idpays2)
                //};
                //_Entreprise.Create_entreprise(entreprise1);
                convention.Entreprise_codePostal = entreprise.CodePostal;
                convention.Entreprise_IdPays = entreprise.Idpays2;
                convention.Entreprise_Ligne1Adresse = entreprise.Ligne1Adresse;
                convention.Entreprise_Ligne2Adresse = entreprise.Ligne2Adresse;
                convention.Entreprise_Ligne3Adresse = entreprise.Ligne3Adresse;
                convention.Entreprise_Mail = entreprise.MailEntreprise;
                convention.Entreprise_raison_social = entreprise.RaisonSociale;
                convention.Entreprise_Tel = entreprise.TelEntreprise;
                convention.Entreprise_Ville = entreprise.Ville;
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
            str = HttpContext.Session.GetString("pro");
            List<Professionnel> pro = new List<Professionnel>();
            if (str == "")
            {
               pro = _pro.Get_Pro(convention.IdEntreprise);
            }

            List<Pro_Session_ModelView> pro_Sessions = new List<Pro_Session_ModelView>();

            foreach (var item in pro)
            {
                Pro_Session_ModelView pro_Session_ = new Pro_Session_ModelView
                {
                    CodeTitreCiviliteProfessionnel = item.CodeTitreCiviliteProfessionnel,
                    NomProfessionnel = item.NomProfessionnel,
                    PrenomProfessionnel = item.PrenomProfessionnel,
                    ID = item.IdProfessionnel
                };
                pro_Sessions.Add(pro_Session_);
            }

            if (str != "")
            {
                List<Pro_Session_ModelView> professionnels = JsonConvert.DeserializeObject<List<Pro_Session_ModelView>>(str);
                foreach (var item in professionnels)
                {
                    Pro_Session_ModelView professionnel = new Pro_Session_ModelView
                    {
                        NomProfessionnel = item.NomProfessionnel,
                        PrenomProfessionnel = item.PrenomProfessionnel,
                        CodeTitreCiviliteProfessionnel = item.CodeTitreCiviliteProfessionnel,
                        Create = item.Create
                    };
                    pro_Sessions.Add(professionnel);
                }
            }
            str = JsonConvert.SerializeObject(pro_Sessions);
            HttpContext.Session.SetString("pro", str);
            return View(pro_Sessions);
        }

        //post Professionel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Professionel(List<Pro_Session_ModelView> obj)
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

            string xd = HttpContext.Session.GetString("pro");
            List<Pro_Session_ModelView> professionnels = JsonConvert.DeserializeObject<List<Pro_Session_ModelView>>(xd);
            if (Request.Form["tuteur"] != "")
            {
                int tuteurID = int.Parse(Request.Form["tuteur"].ToString());
                Pro_Session_ModelView professionnel = new Pro_Session_ModelView();
                professionnel = professionnels[tuteurID];
                convention.TuteurNom = professionnel.NomProfessionnel;
                convention.TuteurPrenom = professionnel.PrenomProfessionnel;
                convention.IdTuteur = professionnel.ID;
                convention.Tuteur_create_Id = tuteurID;
                convention.Tuteur_create = professionnel.Create;
            }
            if (Request.Form["Responsable"] != "")
            {
                int ResponsableID = int.Parse(Request.Form["Responsable"].ToString());
                Pro_Session_ModelView professionnel = new Pro_Session_ModelView();
                professionnel = professionnels[ResponsableID];
                convention.ResponsableNom = professionnel.NomProfessionnel;
                convention.ResponsablePrenom = professionnel.PrenomProfessionnel;
                convention.IdResponsable = professionnel.ID;
                convention.Responsable_create_Id = ResponsableID;
                convention.Responsable_create = professionnel.Create;
            }
            var x = JsonConvert.SerializeObject(convention);
            HttpContext.Session.SetString("convention", x);
            return RedirectToAction("date_create");
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
                List<Pro_Session_ModelView> professionnels = new List<Pro_Session_ModelView>();
                string str = this.HttpContext.Session.GetString("pro");
                if (str != null)
                {
                    professionnels = JsonConvert.DeserializeObject<List<Pro_Session_ModelView>>(str);
                }
                Pro_Session_ModelView pro = new Pro_Session_ModelView
                {
                    AdresseMail = obj.AdresseMail,
                    Create = true,
                    CodeTitreCiviliteProfessionnel = obj.CodeTitreCiviliteProfessionnel,
                    Fonction = obj.Fonction,
                    NomProfessionnel = obj.NomProfessionnel,
                    PrenomProfessionnel = obj.PrenomProfessionnel,
                    NumerosTel = obj.NumerosTel
                };
                professionnels.Add(pro);
                str = JsonConvert.SerializeObject(professionnels);
                HttpContext.Session.SetString("pro", str);
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
            if (str != "")
            {
                listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            }
            date.Iddate = listDate.Count();
            listDate.Add(date);
            str = JsonConvert.SerializeObject(listDate);
            HttpContext.Session.SetString("date", str);
            return RedirectToAction("date");
            //return View(date);
        }

        // get date
        public IActionResult date_delete(int id)
        {
            string str = HttpContext.Session.GetString("date");
            List<Date_ModelView> listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            Date_ModelView date = listDate[id];
            return View(date);
        }

        // get post
        [HttpPost]
        public IActionResult date_delete(Date_ModelView date)
        {
            string str = HttpContext.Session.GetString("date");
            List<Date_ModelView> listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            listDate.RemoveAt(date.Iddate);
            str = JsonConvert.SerializeObject(listDate);
            HttpContext.Session.SetString("date", str);
            return RedirectToAction("date");
        }

        // get recapitulatif
        public IActionResult Recapitulatif()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

            string str_date = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> dates = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date);
            ViewBag.dates = dates;
            ViewBag.convention = convention;

            return View();
        }

        //post Recapitulatif
        [HttpPost]
        public IActionResult Recapitulatif(FilesModel uploadFile)
        {

            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
            int entrepriseID = 0;

            Pee pee = new Pee
            {
                IdEntreprise = convention.IdEntreprise,
                MatriculeBeneficiaire = convention.Idmatricule,
                IdTuteur = convention.IdTuteur,
                IdResponsableJuridique = convention.IdResponsable,
                IdOffreFormation = convention.IdFormation,
                IdEtablissement = convention.IdEtablissement
            };

            if (convention.Entreprise_Create == true)
            {
                Entreprise entreprise = new Entreprise
                {
                    CodePostal = convention.Entreprise_codePostal,
                    Idpays2 = "Fr",
                    Ligne1Adresse = convention.Entreprise_Ligne1Adresse,
                    Ligne2Adresse = convention.Entreprise_Ligne2Adresse,
                    Ligne3Adresse = convention.Entreprise_Ligne3Adresse,
                    RaisonSociale = convention.Entreprise_raison_social,
                    MailEntreprise = convention.Entreprise_Mail,
                    Ville = convention.Entreprise_Ville,
                    TelEntreprise = convention.Entreprise_Tel,
                    NumeroSiret = convention.Siret
                };
                entrepriseID = _Entreprise.Create_entreprise_ID_Back(entreprise);
                pee.IdEntreprise = entrepriseID;
            }

            if(convention.Tuteur_create == true)
            {
                Professionnel professionnel = new Professionnel
                {
                    CodeTitreCiviliteProfessionnel = 1,
                    NomProfessionnel = convention.TuteurNom,
                    PrenomProfessionnel = convention.TuteurPrenom
                };
                convention.IdTuteur = _pro.create_get_ID(professionnel);
            }

            if (convention.Responsable_create == true)
            {
                Professionnel professionnel = new Professionnel
                {
                    CodeTitreCiviliteProfessionnel = 0,
                    NomProfessionnel = convention.ResponsableNom,
                    PrenomProfessionnel = convention.ResponsablePrenom
                };
                convention.IdResponsable = _pro.create_get_ID(professionnel);
            }

            string str_date = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> dates = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date);
            decimal peeId = _peelayer.Pee_Create_ID_Back(pee);
            foreach (var item in dates)
            {
                PeriodePee periodePee = new PeriodePee
                {
                    IdPee = peeId,
                    DateDebutPeriodePee = item.Date1,
                    DateFinPeriodePee = item.Date2,
                    NumOrdre = item.Iddate
                };
                _periode.Pee_Create(periodePee);
            }

            //if (ModelState.IsValid)
            //{
            //    var postedFile = uploadFile.file;
            //    try
            //    {
            //        var Response = UploadFiles.UploadFile(postedFile, Path);

            //        if (Response.Done)
            //        {
            //            return View(viewName: "Index");
            //        }
            //        else
            //        {
            //            return BadRequest();
            //        }

            //    }
            //    catch (Exception e)
            //    {
            //        Response.WriteAsync("<script>alert('" + e + "')</script>");
            //        return View(viewName: "Index");
            //    }
            //}

            return RedirectToAction("index");
        }
        //protected string Path { get; set; }

        //public ConventionController()
        //{
        //    Path = "./Data/Documents";

        //}

    }
}
