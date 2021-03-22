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

                //contrat = _context.Contrats.Where(c => c.IdContrat == destinataireEnquete.IdContrat).FirstOrDefault();
                //contrat.IdEntrepriseNavigation = _context.Entreprises.Where(e => e.IdEntreprise == contrat.IdEntreprise).FirstOrDefault();
                //contrat.TypeContratNavigation = _context.TypeContrats.Where(tc => tc.IdTypeContrat == contrat.TypeContrat).FirstOrDefault();
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

        //[HttpPost]
        //public IActionResult ChercherEntreprise(string siret)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Entreprise entreprise = _Entreprise.get_Entreprise(siret).FirstOrDefault();

        //        if (entreprise is null)
        //        {
        //            return RedirectToAction("Entreprise_creation", "Convention");
        //        }
        //        else
        //        {
        //            string str = HttpContext.Session.GetString("contrat");
        //            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str);
        //            contrat.IdEntreprise = _Entreprise.get_Entreprise(siret).FirstOrDefault().IdEntreprise;
        //            contrat.IdEntrepriseNavigation = _Entreprise.get_Entreprise(siret).FirstOrDefault();
        //            str = JsonConvert.SerializeObject(contrat, Formatting.Indented, new JsonSerializerSettings()
        //            {
        //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //            });

        //            HttpContext.Session.SetString("contrat", str);

        //        }
        //    }
        //        return View();
        //}
        public IActionResult DisplayRecap()
        {
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 

            return View(contrat); 
        }
    }
}
