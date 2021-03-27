﻿using Microsoft.AspNetCore.Mvc;
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

        public ContratController(AFPANADbContext context)
        {
            _Entreprise = new Layer_Entreprise(context);
            _DestinataireEnquete = new Layer_DestinataireEnquete(context);
            _Contrat = new Layer_Contrat(context);
            _TypeContrat = new Layer_TypeContrat(context);
            _Pays = new Layer_Pays(context);
        }

        //récupérer l'idSoumissionnaire du destinataire enquête permet de vérifier si un contrat correspondant existe
        //puis de renseigner la vue en fonction de sa situation
        public IActionResult Display(Guid id)
        {
            DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
            destinataireEnquete = _DestinataireEnquete.GetDestinataireEnqueteByIdSoumissionnaire(id);

            Contrat contrat = new Contrat();

            if (destinataireEnquete.IdContrat == null)
            {
                contrat = null; 
            }
            else
            {
                contrat = _Contrat.GetContratByIdContrat(destinataireEnquete.IdContrat);
                contrat.IdEntrepriseNavigation = _Entreprise.GetEntrepriseById(contrat.IdEntreprise);
                contrat.TypeContratNavigation = _TypeContrat.GetTypeContratById(contrat.TypeContrat); 
            }

            //serialiser séparemment un objet entreprise ?
            string str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }); 

            HttpContext.Session.SetString("contrat", str); 
            
            return View(contrat);
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
                    return RedirectToAction("CreerEntreprise", "Contrat");
                }
                else
                {
                    //serialiser séparemment un objet entreprise ?
                    string str = HttpContext.Session.GetString("contrat");
                    Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);
                    contrat.IdEntreprise = entreprise.IdEntreprise;
                    contrat.IdEntrepriseNavigation = entreprise;
                    str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    HttpContext.Session.SetString("contrat", str);

                    return RedirectToAction("ModifierContrat");
                }
                
            }
            return View(); 
        }

        [HttpGet]
        public IActionResult CreerEntreprise()
        {

            return View();
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

                string str = this.HttpContext.Session.GetString("contrat");
                Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);

                contrat.IdEntrepriseNavigation = entreprise; 
                //serialiser séparemment un objet entreprise ?
                str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("contrat", str);

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
                DateEntreeFonction = contrat.DateEntreeFonction,
                DateSortieFonction = contrat.DateSortieFonction,
                //TypeContrat = contrat.TypeContratNavigation.DesignationTypeContrat
            };

            List<string> typesContratsList = _TypeContrat.GetDesignationsTypeContrat();
            ViewBag.TypesContratsList = typesContratsList; 
            

            return View(contratModelView); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifierContrat(ContratModelView contratModelView)
        {
            if (ModelState.IsValid)
            {
                Contrat contrat = new Contrat
                {
                    DateEntreeFonction = contratModelView.DateEntreeFonction,
                    DateSortieFonction = contratModelView.DateSortieFonction,
                    LibelleFonction = contratModelView.LibelleFonction,
                    //TypeContrat = _TypeContrat.GetIdTypeContratByDesignation(contratModelView.TypeContrat),
                    //TypeContratNavigation = _TypeContrat.GetTypeContratByDesignation(contratModelView.TypeContrat)
                };
                
                //calculer la durée du contrat, le rapport avec la formation (code Rome) et le code Appellation

                string str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                HttpContext.Session.SetString("contrat", str);
                
            }

            return View();
        }

        [HttpGet]
        public IActionResult DisplayRecap()
        {
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 

            return View(contrat); 
        }
        //to do : serialisation/deserialization d'entreprise aux bons endroits
        //autocomplete de la textbox appellationRome : créer api puis script jquery
        //résoudre le mystère de la dropdownlist
        //compléter la méthode récap GET
        //écrire la méthode récap POST = enregistrement en bdd du contrat (create ou update) et éventuellement de l'entreprise (create)
    }
}
