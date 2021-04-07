using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AppAfpaBrive.DAL.Layer;
using AppAfpaBrive.DAL.Layers;
using AppAfpaBrive.Web.Layers; 
using AppAfpaBrive.Web.ModelView;


namespace AppAfpaBrive.Web.Controllers
{
    public class ContratController : Controller
    {
        private Layer_Entreprise _Entreprise = null;
        private Layer_DestinataireEnquete _DestinataireEnquete = null;
        private Layer_Contrat _Contrat = null;
        private Layer_TypeContrat _TypeContrat = null;
        private Layer_Pays _Pays = null;
        private Layer_AppelationRomes _AppelationRomes = null ;
        private Layer_Code_Produit_Formation _Code_Produit_Formation = null;
        private AFPANADbContext _Db = new AFPANADbContext(); 

        public ContratController(AFPANADbContext context)
        {
            _Entreprise = new Layer_Entreprise(context);
            _DestinataireEnquete = new Layer_DestinataireEnquete(context);
            _Contrat = new Layer_Contrat(context);
            _TypeContrat = new Layer_TypeContrat(context);
            _Pays = new Layer_Pays(context);
            _AppelationRomes = new Layer_AppelationRomes(context);
            _Code_Produit_Formation = new Layer_Code_Produit_Formation(context); 
        }

       //le point de départ est le destinataire de l'enquête et son identifiant unique
        public IActionResult Display(Guid id)//l'identifiant unique est récupéré via une query string ? 
        {   
            DestinataireEnquete destinataireEnquete = _DestinataireEnquete.GetDestinataireEnqueteByIdSoumissionnaire(id);
            if (destinataireEnquete is null)
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé."); 
            }

            Contrat contrat = _Contrat.GetContratByIdContrat(destinataireEnquete.IdContrat);
            Entreprise entreprise = new Entreprise();
            bool ContratIsNew; //pour enregistrement final en bdd, il faut parvenir à déterminer si le contrat doit être créé ou modifié
            if (contrat is null)
            {
                //si aucun contrat n'existe en db pour le destinataireEnquete,
                //on le créé et on lui passe le MatriculeBeneficiaire
                //on créé aussi une entreprise qui pour l'instant est null
                contrat = new Contrat
                {
                    MatriculeBeneficiaire = destinataireEnquete.MatriculeBeneficiaire,
                    
                };
                entreprise = null;
                ContratIsNew = true;
            }
            else
            {
                //si le contrat existe on complète ses propriétés de navigation
                contrat.IdEntrepriseNavigation = _Entreprise.GetEntrepriseById(contrat.IdEntreprise);
                contrat.TypeContratNavigation = _TypeContrat.GetTypeContratById(contrat.TypeContrat);
                entreprise = _Entreprise.GetEntrepriseById(contrat.IdEntreprise);
                ContratIsNew = false; 
            }

            #region Sérialization et passage en cookies de session des objets destinataireEnquete et Contrat
            //sérialisation du destinataireEnquête
            string strDestinataireEnquete = JsonConvert.SerializeObject(destinataireEnquete, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore //évite que les objet qui font référence à des listes qui les contiennent eux-mêmes ne déclenchent une erreur de sérialization
            });
            HttpContext.Session.SetString("destinataireEnquete", strDestinataireEnquete);

            //sérialisation de l'objet Contrat
            string str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }); 
            HttpContext.Session.SetString("contrat", str);

            //sérialisation du booléen contratIsNew
            string contratIsNew = JsonConvert.SerializeObject(ContratIsNew); 
            HttpContext.Session.SetString("contratIsNew", contratIsNew);

            //serialisation de l'objet entreprise, même s'il peut être null. Il vaut mieux créer dès le départ un cookie d'entreprise
            //pour tous le monde car à terme, il est utilisé pour tous les utilsateurs à un moment où un autre
            string strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("entreprise", strEntreprise);
            #endregion

            return View(contrat);//on retourne le contrat pour affichage
        }

        [HttpGet]
        public IActionResult ChercherEntreprise()
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");

            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }

            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChercherEntreprise(Entreprise_Siret siret)
        {
            if (ModelState.IsValid)
            {
                string strEntreprise = HttpContext.Session.GetString("entreprise");
                Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);
                entreprise = _Entreprise.get_Entreprise(siret.NumeroSiret).FirstOrDefault();

                if (entreprise is null)
                {
                    //si l'entreprise n'est pas en bdd, on se contente de sérialiser le siret
                    //saisi par l'utilisateur pour le passer à la page de création
                    //on resérialise entreprise même si elle est toujours null
                    string strSiret = JsonConvert.SerializeObject(siret, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("siret", strSiret);

                    strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("entreprise", strEntreprise);

                    return RedirectToAction("CreerEntreprise");//renvoi vers la page de création d'entreprise
                }
                else
                {
                    //si l'entreprise existe, on désérialise l'objet contrat pour lui ajouter l'id de l'entreprise et
                    //la lui passer en propriété de navigation, puis on resérialise le contrat et l'entreprise
                    //pour les passer à la page de modif
                    string str = HttpContext.Session.GetString("contrat");
                    Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

                    contrat.IdEntreprise = entreprise.IdEntreprise;
                    contrat.IdEntrepriseNavigation = entreprise;

                    str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("contrat", str);

                    strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("entreprise", strEntreprise);

                    return RedirectToAction("ModifierContrat");//renvoi vers la page de modification du contrat
                }
                
               
            }
            return View(); 
        }

        [HttpGet]
        public IActionResult CreerEntreprise()
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");

            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }

            string str = HttpContext.Session.GetString("siret");
            Entreprise_Siret siret = JsonConvert.DeserializeObject<Entreprise_Siret>(str);

            Entreprise_Creation_ViewModel ecv = new Entreprise_Creation_ViewModel
            {
                NumeroSiret = siret.NumeroSiret
            };
            return View(ecv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreerEntreprise(Entreprise_Creation_ViewModel entrepriseViewModel)
        {
            if (ModelState.IsValid)
            {
                string strEntreprise = HttpContext.Session.GetString("entreprise");
                Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);
                if (entreprise is null)
                {
                    entreprise = new Entreprise(); 
                }

                entreprise.NumeroSiret = entrepriseViewModel.NumeroSiret;
                entreprise.CodePostal = entrepriseViewModel.CodePostal;
                entreprise.Ligne1Adresse = entrepriseViewModel.Ligne1Adresse;
                entreprise.Ligne2Adresse = entrepriseViewModel.Ligne2Adresse;
                entreprise.Ligne3Adresse = entrepriseViewModel.Ligne3Adresse;
                entreprise.RaisonSociale = entrepriseViewModel.RaisonSociale;
                entreprise.Ville = entrepriseViewModel.Ville;
                entreprise.Idpays2 = _Pays.Get_pays_ID(entrepriseViewModel.Idpays2); 

                #region Désérialisation/Sérialisation des objets Contrat et Entreprise
                //Désérialisation/sérialisation de Contrat pour lui passer l'entreprise créée en propriété de navigation.
                //Est-ce bien approprié de le faire ici ? L'entreprise n'a pas encore d'id et n'est pas encore en bdd...
                string str = this.HttpContext.Session.GetString("contrat");
                Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

                contrat.IdEntrepriseNavigation = entreprise; 
                
                str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("contrat", str);

                //sérialisation de l'objet entreprise 
                strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("entreprise", strEntreprise);

                #endregion

                return RedirectToAction("ModifierContrat"); 
            }
            return View();
        }

        [HttpGet]
        public IActionResult ModifierContrat()
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");

            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }

            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);
            //normalement, arrivé à ce point, aucun cookie entreprise n'est null
            string strEntreprise = HttpContext.Session.GetString("entreprise");
            Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);

            ContratModelView contratModelView = new ContratModelView
            {
                LibelleFonction = contrat.LibelleFonction,
                DateEntreeFonction = DateTime.Now,
                DateSortieFonction = contrat.DateSortieFonction,
                TypeContrat = contrat.TypeContrat.ToString(),
                TypesContrats = _TypeContrat.GetAllToDropDownList(),
                Entreprise = entreprise
            };

            str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("contrat", str);

            strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("entreprise", strEntreprise);

            return View(contratModelView); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifierContrat(ContratModelView contratModelView)
        {
            if (ModelState.IsValid)
            {
                string str = this.HttpContext.Session.GetString("contrat");
                Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

                contrat.DateEntreeFonction = contratModelView.DateEntreeFonction;
                contrat.DateSortieFonction = contratModelView.DateSortieFonction;
                contrat.LibelleFonction = contratModelView.LibelleFonction;
                contrat.TypeContrat = int.Parse(contratModelView.TypeContrat);
                contrat.TypeContratNavigation = _TypeContrat.GetTypeContratById(int.Parse(contratModelView.TypeContrat)); 
                
                var appelationRome = _AppelationRomes.GetAppellationRomeByLibelle(contrat.LibelleFonction);
                contrat.CodeAppellation = appelationRome.CodeAppelationRome;

                string strDestinataire = HttpContext.Session.GetString("destinataireEnquete");
                DestinataireEnquete de = JsonConvert.DeserializeObject<DestinataireEnquete>(strDestinataire);

                var codesRomeDestinataire = _Code_Produit_Formation.GetCodeRomeProduitFormationByDestinataireEnquete(de);
                var codeRomeContrat = _AppelationRomes.GetAppellationRomeByLibelle(contrat.LibelleFonction); 

                if (codesRomeDestinataire.Any(crd=>crd.Contains(codeRomeContrat.CodeRome)))
                {
                    contrat.EnLienMetierFormation = true;
                }

                if (contrat.DateSortieFonction is not null)
                {
                    contrat.DureeContratMois = (contrat.DateSortieFonction.Value.Month - contrat.DateEntreeFonction.Month)
                        + 12 * (contrat.DateSortieFonction.Value.Year - contrat.DateEntreeFonction.Year); 
                }

                str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("contrat", str);

                return RedirectToAction("DisplayRecap"); 
            }
            contratModelView.TypesContrats = _TypeContrat.GetAllToDropDownList();

            return View(contratModelView);
        }

        [HttpGet]
        public IActionResult DisplayRecap()
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");

            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }

            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 
            string strEntreprise = this.HttpContext.Session.GetString("entreprise");
            Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);
            

            RecapModelView recap = new RecapModelView
            {
                Entreprise = entreprise,
                Contrat = contrat
            };

            str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("contrat", str);

            strEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("entreprise", strEntreprise);

            return View(recap); 
        }

        [HttpPost]
        public IActionResult DisplayRecap(RecapModelView recap)
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");
            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }
            //deserialisation de l'objet entreprise 
            string strEntreprise = this.HttpContext.Session.GetString("entreprise");
            Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);
            //si l'entreprise contenue en cookie n'existe pas en db, elle est créée 
            if (!_Entreprise.GetAllSirets().Contains(entreprise.NumeroSiret))
            {
                _Db.Entry(entreprise).State = EntityState.Added;
            }

            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);
            contrat.IdEntrepriseNavigation = entreprise;
            string contratIsNew = this.HttpContext.Session.GetString("contratIsNew");
            bool ContratIsNew = JsonConvert.DeserializeObject<bool>(contratIsNew);
            if (ContratIsNew)
            {
                //création du contrat en db s'il est nouveau
                _Db.Entry(contrat).State = EntityState.Added; 
            }
            else
            {
                //update du contrat en db s'il existe déjà
                _Db.Entry(contrat).State = EntityState.Modified; 
            }

            
            DestinataireEnquete destinataireEnquete = JsonConvert.DeserializeObject<DestinataireEnquete>(strDestinataire);
            destinataireEnquete.IdContratNavigation = contrat;
            if (contrat.DateSortieFonction is null || contrat.DateSortieFonction > DateTime.Now)
            {
                destinataireEnquete.EnEmploi = true;

            }

            _Db.Entry(destinataireEnquete).State = EntityState.Modified;

            _Db.SaveChanges();

            string strDestinataireEnquete = JsonConvert.SerializeObject(destinataireEnquete, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("destinataireEnquete", strDestinataireEnquete);
            ////validation des infos, toutes les propriétés de navigation doivent être renseignées,
            ////création ou modification des objets selon contexte

            return RedirectToAction("AuRevoir");
        }

        [HttpGet]
        public IActionResult AuRevoir()
        {
            string strDestinataire = this.HttpContext.Session.GetString("destinataireEnquete");
            if (string.IsNullOrEmpty(strDestinataire))
            {
                return BadRequest("Vous n'êtes pas authentifié en tant que destinataire de notre enquête. " +
                    "Veuillez y accéder via le lien qui vous a été adressé.");
            }
            DestinataireEnquete destinataireEnquete = JsonConvert.DeserializeObject<DestinataireEnquete>(strDestinataire);
            //on incrémente le nombre de réponses données pendant la campagne de mailing de 1
            PlanificationCampagneMail pcm = _Db.PlanificationCampagneMails
                .Where(pcm => pcm.IdCampagneMail == destinataireEnquete.IdPlanificationCampagneMail)
                .FirstOrDefault();
            pcm.NombreReponses++;
            _Db.Entry(pcm).State = EntityState.Modified;
            _Db.SaveChanges();

            return View();
        }


        
        //faire GET et POST pour la vue AuRevoir : où signaler que le destiantaireEnquete accepte d'être contacté par les élèves ?
        //où se trouve le mécanisme de désinscription ? 
    }
}
