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
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.Web.Utilitaires;
using System.IO;
using static AppAfpaBrive.Web.Layers.Layer_Pee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AppAfpaBrive.Web.Controllers.Convention
{
    [Authorize(Roles = "Bénéficiaire")]
    public class ConventionController : Controller
    {
        private readonly Layer_BeneficiaireOffreFormation _beneficiaireOffre = null;
        private readonly Layer_Etablissement _Etablissement = null;
        private readonly Layer_Produit_Formation _Produit_Formation = null;
        private readonly Layer_Entreprise _Entreprise = null;
        private readonly Layer_Pays _Pays = null;
        private readonly Layer_Professionnel _pro = null;
        private readonly Layer_EntrepriseProfessionnel _entreprisepro = null;
        private readonly Layer_Pee _peelayer = null;
        private readonly Layer_Periode_pee _periode = null;
        private readonly Layer_PeeDocument _PeeDocument = null;
        private readonly IConfiguration _config;
        private readonly AFPANADbContext _context = null;

        public ConventionController(AFPANADbContext context, IConfiguration config)
        {
            _beneficiaireOffre = new Layer_BeneficiaireOffreFormation(context);
            _Etablissement = new Layer_Etablissement(context);
            _Produit_Formation = new Layer_Produit_Formation(context);
            _Entreprise = new Layer_Entreprise(context);
            _Pays = new Layer_Pays(context);
            _pro = new Layer_Professionnel(context);
            _entreprisepro = new Layer_EntrepriseProfessionnel(context);
            _peelayer = new Layer_Pee(context);
            _periode = new Layer_Periode_pee(context);
            _PeeDocument = new Layer_PeeDocument(context);
            _config = config;
            _context = context;
        }



        // get index
        public IActionResult Index()
        {
            string matricule = User.Identity.Name;
            IEnumerable<BeneficiaireOffreFormation> beneficiaires = _beneficiaireOffre.GetFormations(matricule);
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
                    Etablissement = _Etablissement.Get_Etablissement_Nom_Etablissement(item.Idetablissement),
                    Formation = _Produit_Formation.Get_Formation_Nom(item.IdOffreFormation).FirstOrDefault()
                };
                obj.Add(convention);
            }
            Creation_convention Session_Convention = new Creation_convention
            {
                Idmatricule = matricule
            };
            var str = JsonConvert.SerializeObject(Session_Convention);
            HttpContext.Session.SetString("convention", str);
            return View(obj);
        }

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
            convention.IdEtablissement = _beneficiaireOffre.GetIdetablissemnt_Id_Etablissement(convention.Idmatricule, id);
            convention.Etablissement = _Etablissement.Get_Etablissement_Nom_Etablissement(convention.IdEtablissement);
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
                return RedirectToAction("Entreprise_Recap");
            }
            return View(obj);
        }


        // get Entreprise_Recap
        public IActionResult Entreprise_Recap()
        {
            string str = this.HttpContext.Session.GetString("convention");
            Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);
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
            Entreprise_Creation_ViewModel entreprise = new Entreprise_Creation_ViewModel()
            {
                NumeroSiret = siret
            };
            IQueryable<string> pays = _Pays.Get_pays();
            return View(entreprise);
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
                convention.Entreprise_codePostal = entreprise.CodePostal;
                convention.Entreprise_IdPays = _Pays.Get_pays_ID(entreprise.Idpays2);
                convention.Entreprise_Ligne1Adresse = entreprise.Ligne1Adresse;
                convention.Entreprise_Ligne2Adresse = entreprise.Ligne2Adresse;
                convention.Entreprise_Ligne3Adresse = entreprise.Ligne3Adresse;
                convention.Entreprise_Mail = entreprise.MailEntreprise;
                convention.Entreprise_raison_social = entreprise.RaisonSociale;
                convention.Entreprise_Tel = entreprise.TelEntreprise;
                convention.Entreprise_Ville = entreprise.Ville;
                convention.Siret = entreprise.NumeroSiret;
                convention.IdEntreprise = 0;
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

            List<Pro_Session_ModelView> pro_Sessions = new List<Pro_Session_ModelView>();
            // 1er fois
            if (str == "")
            {
                List<Professionnel> pro = _pro.Get_Pro(convention.IdEntreprise);
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
            }
            else
            {
                List<Pro_Session_ModelView> professionnels = JsonConvert.DeserializeObject<List<Pro_Session_ModelView>>(str);
                foreach (var item in professionnels)
                {
                    Pro_Session_ModelView professionnel = new Pro_Session_ModelView
                    {
                        NomProfessionnel = item.NomProfessionnel,
                        PrenomProfessionnel = item.PrenomProfessionnel,
                        CodeTitreCiviliteProfessionnel = item.CodeTitreCiviliteProfessionnel,
                        ID = item.ID,
                        Create = item.Create,
                        AdresseMail = item.AdresseMail,
                        Fonction = item.Fonction,
                        NumerosTel = item.NumerosTel
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
                convention.Tuteur_AdresseMail = professionnel.AdresseMail;
                convention.Tuteur_Fonction = professionnel.Fonction;
                convention.Tuteur_Telephone = professionnel.NumerosTel;
                convention.Tuteur_genre = professionnel.CodeTitreCiviliteProfessionnel;
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
                convention.Responsable_AdresseMail = professionnel.AdresseMail;
                convention.Responsable_Fonction = professionnel.Fonction;
                convention.Responsable_Telephone = professionnel.NumerosTel;
                convention.Responsable_genre = professionnel.CodeTitreCiviliteProfessionnel;
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
            if (date == null)
            {
                return RedirectToAction("professionel");
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
            if (ModelState.IsValid)
            {
                List<Date_ModelView> listDate = new List<Date_ModelView>();
                string str = this.HttpContext.Session.GetString("date");
                if (str != "")
                {
                    listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
                }
                date.Iddate = listDate.Count() + 1;
                listDate.Add(date);
                str = JsonConvert.SerializeObject(listDate);
                HttpContext.Session.SetString("date", str);
                return RedirectToAction("date");
            }
            return View(date);
        }

        // get date
        public IActionResult date_delete(int id)
        {
            string str = HttpContext.Session.GetString("date");
            List<Date_ModelView> listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            Date_ModelView date = listDate[id - 1];
            return View(date);
        }

        // get post
        [HttpPost]
        public IActionResult date_delete(Date_ModelView date)
        {
            string str = HttpContext.Session.GetString("date");
            List<Date_ModelView> listDate = JsonConvert.DeserializeObject<List<Date_ModelView>>(str);
            listDate.RemoveAt(date.Iddate - 1);
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
        public async Task<IActionResult> Recapitulatif(FilesModelConvention uploadFile)
        {
            if (ModelState.IsValid)
            {
                string str = this.HttpContext.Session.GetString("convention");
                Creation_convention convention = JsonConvert.DeserializeObject<Creation_convention>(str);

                Entreprise entreprise = new Entreprise();
                Professionnel Tuteur = new Professionnel();
                EntrepriseProfessionnel tuteur_entr = new EntrepriseProfessionnel();
                Professionnel Responsable = new Professionnel();
                EntrepriseProfessionnel Responsable_entr = new EntrepriseProfessionnel();
                PeriodePee periodePee = new PeriodePee();
                entreprise = new Entreprise
                {
                    Ligne1Adresse = convention.Entreprise_Ligne1Adresse,
                    Ligne2Adresse = convention.Entreprise_Ligne2Adresse,
                    Ligne3Adresse = convention.Entreprise_Ligne3Adresse,
                    CodePostal = convention.Entreprise_codePostal,
                    MailEntreprise = convention.Entreprise_Mail,
                    Idpays2 = convention.Entreprise_IdPays,
                    NumeroSiret = convention.Siret,
                    RaisonSociale = convention.Entreprise_raison_social,
                    Ville = convention.Entreprise_Ville,
                    TelEntreprise = convention.Entreprise_Tel
                };

                Tuteur = new Professionnel
                {
                    PrenomProfessionnel = convention.TuteurPrenom,
                    NomProfessionnel = convention.TuteurNom,
                    CodeTitreCiviliteProfessionnel = convention.Tuteur_genre,
                    IdProfessionnel = convention.IdTuteur
                };

                tuteur_entr = new EntrepriseProfessionnel
                {
                    AdresseMailPro = convention.Tuteur_AdresseMail,
                    Fonction = convention.Tuteur_Fonction,
                    TelephonePro = convention.Tuteur_Telephone,
                    IdProfessionnelNavigation = Tuteur,
                    IdEntrepriseNavigation = entreprise
                };

                Responsable = new Professionnel
                {
                    PrenomProfessionnel = convention.ResponsablePrenom,
                    NomProfessionnel = convention.ResponsableNom,
                    CodeTitreCiviliteProfessionnel = convention.Responsable_genre,
                    IdProfessionnel = convention.IdResponsable
                };

                Responsable_entr = new EntrepriseProfessionnel
                {
                    AdresseMailPro = convention.Responsable_AdresseMail,
                    Fonction = convention.Responsable_Fonction,
                    TelephonePro = convention.Responsable_Telephone,
                    IdProfessionnelNavigation = Tuteur,
                    IdEntrepriseNavigation = entreprise
                };

                Pee pee = new Pee
                {
                    IdEntrepriseNavigation = entreprise,
                    MatriculeBeneficiaire = convention.Idmatricule,
                    IdOffreFormation = convention.IdFormation,
                    IdEtablissement = convention.IdEtablissement
                };

                if (convention.Entreprise_Create == true)
                {
                    _context.Entry(entreprise).State = EntityState.Added;
                }
                else
                {
                    entreprise.IdEntreprise = convention.IdEntreprise;
                    _context.Entry(entreprise).State = EntityState.Unchanged;
                }


                string str_date = this.HttpContext.Session.GetString("date");
                List<Date_ModelView> dates = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date);
                foreach (var item in dates)
                {
                    periodePee = new PeriodePee
                    {
                        IdPeeNavigation = pee,
                        DateDebutPeriodePee = item.Date1,
                        DateFinPeriodePee = item.Date2,
                        NumOrdre = item.Iddate
                    };
                    _context.Entry(periodePee).State = EntityState.Added;
                }

                if (convention.Tuteur_create == true)
                {
                    pee.IdTuteurNavigation = Tuteur;
                    tuteur_entr.IdProfessionnelNavigation = Tuteur;
                    if (convention.Entreprise_Create == true)
                    {
                        tuteur_entr.IdEntrepriseNavigation = entreprise;
                        entreprise.EntrepriseProfessionnels.Add(tuteur_entr);
                    }
                    else
                    {
                        tuteur_entr.IdEntreprise = entreprise.IdEntreprise;
                    }

                    _context.Entry(Tuteur).State = EntityState.Added;
                    _context.Entry(tuteur_entr).State = EntityState.Added;
                }
                else
                {
                    pee.IdTuteur = convention.IdTuteur;
                }

                if (convention.Responsable_create == true)
                {
                    pee.IdResponsableJuridiqueNavigation = Responsable;
                    Responsable_entr.IdProfessionnelNavigation = Responsable;
                    Responsable_entr.IdEntreprise = entreprise.IdEntreprise;
                    if (convention.Entreprise_Create == true)
                    {
                        Responsable_entr.IdEntrepriseNavigation = entreprise;
                        if (convention.Tuteur_create_Id != convention.Responsable_create_Id)
                        {
                            entreprise.EntrepriseProfessionnels.Add(Responsable_entr);
                        }
                    }
                    else
                    {
                        Responsable_entr.IdEntreprise = entreprise.IdEntreprise;
                    }

                    if (convention.Tuteur_create_Id == convention.Responsable_create_Id)
                    {
                        pee.IdResponsableJuridiqueNavigation = Tuteur;
                        Responsable_entr = tuteur_entr;
                        entreprise.EntrepriseProfessionnels.Add(Responsable_entr);
                    }
                    else
                    {
                        _context.Entry(Responsable).State = EntityState.Added;
                        _context.Entry(Responsable_entr).State = EntityState.Added;
                    }
                }
                else
                {
                    pee.IdResponsableJuridique = convention.IdResponsable;
                }


                _context.Entry(pee).State = EntityState.Added;
                _context.Entry(periodePee).State = EntityState.Added;
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    return RedirectToAction("Erreur");
                }

                int i = 0;
                try
                {
                    if (uploadFile.file != null)
                    {


                        foreach (var item in uploadFile.file)
                        {
                            var postedFile = item;
                            i++;
                            if (postedFile != null)
                            {
                                string get_path = _config.GetSection("PeeDocument").Value;
                                string Path = get_path + pee.IdPee;
                                if (!Directory.Exists(Path))
                                {
                                    Directory.CreateDirectory(Path);
                                }
                                var Response = await UploadFiles.UploadFile(postedFile, Path);

                                if (Response.Done)
                                {
                                    PeeDocument peeDocument = new PeeDocument
                                    {
                                        IdPee = pee.IdPee,
                                        PathDocument = Path + "/" + item.FileName,
                                        NumOrdre = i
                                    };
                                    _PeeDocument.create(peeDocument);
                                }
                                else
                                {
                                    return BadRequest();
                                }

                            }
                            else
                            {
                                return RedirectToAction("Reussite");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Erreur");
                }
                return RedirectToAction("Reussite");
            }

            string str2 = this.HttpContext.Session.GetString("convention");
            Creation_convention convention2 = JsonConvert.DeserializeObject<Creation_convention>(str2);

            string str_date2 = this.HttpContext.Session.GetString("date");
            List<Date_ModelView> dates2 = JsonConvert.DeserializeObject<List<Date_ModelView>>(str_date2);
            ViewBag.dates = dates2;
            ViewBag.convention = convention2;

            return View();
        }

        // get Reussite
        public IActionResult Reussite()
        {
            return View();
        }

        // get Erreur
        public IActionResult Erreur()
        {
            return View();
        }

    }
}
