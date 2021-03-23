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

        public ContratController(AFPANADbContext context)
        {
            _Entreprise = new Layer_Entreprise(context);
            _DestinataireEnquete = new Layer_DestinataireEnquete(context);
            _Contrat = new Layer_Contrat(context);
            _TypeContrat = new Layer_TypeContrat(context); 
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
        public IActionResult ChercherEntreprise(Entreprise_Siret siret)
        {
            if (ModelState.IsValid)
            {
                Entreprise entreprise = _Entreprise.get_Entreprise(siret.NumeroSiret).FirstOrDefault();

                if (entreprise is null)
                {
                    return RedirectToAction("Entreprise_creation", "Convention");
                }
                else
                {
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
        public IActionResult ModifierContrat()
        {

            return View() ; 
        }


        //To do : créer méthode post ModifierContrat avec contrôles de vaidation (création d'un ModelView Contrat?)

        public IActionResult DisplayRecap()
        {
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 

            return View(contrat); 
        }
    }
}
