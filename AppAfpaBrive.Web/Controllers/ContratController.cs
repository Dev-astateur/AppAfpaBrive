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

namespace AppAfpaBrive.Web.Controllers
{
    public class ContratController : Controller
    {
        private AFPANADbContext _context = new AFPANADbContext(); 
        //récupérer l'idSoumissionnaire du destinataire enquête permet de vérifier si un contrat correspondant existe
        //puis de renseigner la vue en fonction de sa situation
        public IActionResult Display(Guid id)
        {
            
            
            DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
            destinataireEnquete = _context.DestinataireEnquetes.Where(de => de.IdSoumissionnaire == id).FirstOrDefault();

            Contrat contrat = new Contrat();

            if (destinataireEnquete.IdContrat == null)
            {
                contrat = null; 
            }
            else
            {
                contrat = _context.Contrats.Where(c => c.IdContrat == destinataireEnquete.IdContrat).FirstOrDefault();
                contrat.IdEntrepriseNavigation = _context.Entreprises.Where(e => e.IdEntreprise == contrat.IdEntreprise).FirstOrDefault();
                contrat.TypeContratNavigation = _context.TypeContrats.Where(tc => tc.IdTypeContrat == contrat.TypeContrat).FirstOrDefault(); 
            }

            string str = JsonConvert.SerializeObject(contrat);
            HttpContext.Session.SetString("contrat", str); 
            
            return View(contrat);
        }

        public IActionResult DisplayRecap()
        {
            string str = this.HttpContext.Session.GetString("contrat");
            Contrat contrat = JsonConvert.DeserializeObject<Contrat>(str); 

            return View(contrat); 
        }
    }
}
