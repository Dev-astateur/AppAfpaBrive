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
            //prévoir un if destinataireEnquete is null ?
            DestinataireEnquete destinataireEnquete = _DestinataireEnquete.GetDestinataireEnqueteByIdSoumissionnaire(id);
            Contrat contrat = _Contrat.GetContratByIdContrat(destinataireEnquete.IdContrat); ;

            if (contrat is null)
            {
                //si aucun contrat n'existe en db pour le destinataireEnquete,
                //on le créé et on lui passe le MatriculeBeneficiaire
                contrat = new Contrat
                {
                    MatriculeBeneficiaire = destinataireEnquete.MatriculeBeneficiaire,
                    
                };
            }
            else
            {
                //si le contrat existe on complète ses propriétés de navigation
                contrat.IdEntrepriseNavigation = _Entreprise.GetEntrepriseById(contrat.IdEntreprise);
                contrat.TypeContratNavigation = _TypeContrat.GetTypeContratById(contrat.TypeContrat);
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
            #endregion

            return View(contrat);//on retourne le contrat pour affichage
        }

        [HttpGet]
        public IActionResult ChercherEntreprise()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChercherEntreprise(Entreprise_Siret siret)
        {
            if (ModelState.IsValid)
            {
                Entreprise entreprise = _Entreprise.get_Entreprise(siret.NumeroSiret).FirstOrDefault();

                if (entreprise is null)
                {
                    //si l'entreprise n'est pas en bdd, on se contente de sérialiser le siret
                    //saisi par l'utilisateur pour le passer à la page de création
                    string strSiret = JsonConvert.SerializeObject(siret, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("siret", strSiret);

                    return RedirectToAction("CreerEntreprise");//renvoi vers la page de création d'entreprise
                }
                else
                {
                    //si l'entreprise existe, on désérialise l'objet contrat pour lui ajouter l'id de l'entreprise et
                    //la lui passer en propriété de navigation, puis on resérialise le contrat pour le passer à la page de modif
                    string str = HttpContext.Session.GetString("contrat");
                    Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

                    contrat.IdEntreprise = entreprise.IdEntreprise;
                    contrat.IdEntrepriseNavigation = entreprise;

                    str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("contrat", str);

                    return RedirectToAction("ModifierContrat");//renvoi vers la page de modification du contrat
                }
                
            }
            return View(); 
        }

        [HttpGet]
        public IActionResult CreerEntreprise()
        {
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
                Entreprise entreprise = new Entreprise
                {
                    NumeroSiret = entrepriseViewModel.NumeroSiret,
                    CodePostal = entrepriseViewModel.CodePostal,
                    Ligne1Adresse = entrepriseViewModel.Ligne1Adresse,
                    Ligne2Adresse = entrepriseViewModel.Ligne2Adresse,
                    Ligne3Adresse = entrepriseViewModel.Ligne3Adresse,
                    RaisonSociale = entrepriseViewModel.RaisonSociale,
                    Ville = entrepriseViewModel.Ville,
                    Idpays2 = _Pays.Get_pays_ID(entrepriseViewModel.Idpays2)
                };

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
                string strNouvelleEntreprise = JsonConvert.SerializeObject(entreprise, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("nouvelleEntreprise", strNouvelleEntreprise);

                #endregion

                return RedirectToAction("ModifierContrat"); 
            }
            return View();
        }

        [HttpGet]
        public IActionResult ModifierContrat()
        {
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

            ContratModelView contratModelView = new ContratModelView
            {
                LibelleFonction = contrat.LibelleFonction,
                DateEntreeFonction = DateTime.Now,
                DateSortieFonction = contrat.DateSortieFonction,
                TypeContrat = contrat.TypeContrat.ToString(),
                TypesContrats = _TypeContrat.GetAllToDropDownList()
            };

            str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            HttpContext.Session.SetString("contrat", str);

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
                contrat.TypeContratNavigation = _TypeContrat.GetTypeContratByDesignation(contratModelView.TypeContrat); //reste null. Pourquoi ? Peut-être qu'il faut instancier un objet TypeContrat ? 
                
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
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 
            string strEntreprise = this.HttpContext.Session.GetString("entreprise");
            Entreprise entreprise = JsonConvert.DeserializeObject<Entreprise>(strEntreprise);
            //créer un ModelView regroupant contrat et entreprise ? T_T

            return View(contrat); 
        }
        //to do : serialisation/deserialization d'entreprise aux bons endroits
        //compléter la méthode récap GET (ajout désérializations supplémentaires si nécessaires)
        //écrire la méthode récap POST = enregistrement en bdd du contrat (create ou update) et éventuellement de l'entreprise (create)
        //vérifier qu'il n'y ait de problème de null à aucun endroit (bad request ?)
    }
}
