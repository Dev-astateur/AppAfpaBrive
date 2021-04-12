using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.Calendar;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AppAfpaBrive.Web.Controllers.Calendrier
{
    [Authorize(Roles = "Formateur,CollaborateurAFPA,Administrateur")]
    public class EvenementFormateurController : Controller
    {
        private readonly Layer_CategorieEvenement _categorieEvenementLayer;
        private readonly Layer_Evenement _evenementLayer;
        private readonly IConfiguration _config;
        public EvenementFormateurController(AFPANADbContext context, IConfiguration config)
        {
            _categorieEvenementLayer = new Layer_CategorieEvenement(context);
            _evenementLayer = new Layer_Evenement(context);
            _config = config;
            
        }

        public IActionResult Create()
        {
            EvenementModelView modelView = new EvenementModelView();
            modelView.SelectListItems = _categorieEvenementLayer.GetTypeEvenements();
            modelView.DateEvent = DateTime.Now;         
            return View(modelView);
        }
        [HttpPost]
        public IActionResult Create(EvenementModelView modelView, string recurrent)
        
        {
            if (ModelState.IsValid)
            {
                string modif = "modification";
                Evenement evenement = new Evenement();
                evenement.DateEvent = modelView.DateEvent;
                evenement.IdCategorieEvent =modelView.IdCategorieEvent;
                evenement.Titre = modelView.Titre;
                evenement.DétailsEvent = modelView.DétailsEvent;
                evenement.IdEtablissement = _config.GetSection("Etablissement").Value;
                evenement.Heure = modelView.Heure;

                if (recurrent == "True")
                {
                    evenement.IdGroupe = Guid.NewGuid();
                    DateTime nouvelleDate=new DateTime(2000,01,01);
                    DateTime dateFin=(DateTime)modelView.DateEventFin;
                    int i = 0;
                    do
                    {
                       
                        if (i==0)
                        {
                            nouvelleDate = evenement.DateEvent;
                        }
                        nouvelleDate = nouvelleDate.AddDays(7);
                        Evenement evenementRecurrent = new Evenement();
                        evenementRecurrent.DateEvent = modelView.DateEvent;
                        evenementRecurrent.IdCategorieEvent = modelView.IdCategorieEvent;
                        evenementRecurrent.Titre = modelView.Titre;
                        evenementRecurrent.DétailsEvent = modelView.DétailsEvent;
                        evenementRecurrent.IdEtablissement = _config.GetSection("Etablissement").Value;
                        evenementRecurrent.IdGroupe = evenement.IdGroupe;
                        evenementRecurrent.Heure = evenement.Heure;

                        evenementRecurrent.DateEvent = nouvelleDate;                       
                        _evenementLayer.AddEvenement(evenementRecurrent);
                        i++;

                    } while (nouvelleDate<= dateFin.AddDays(-7));

                }
                else
                {
                    evenement.IdGroupe = null;
                }


                ViewData["paragrapheAjout"] = modif;
                //_evenementLayer.AddEvenement(modelView);
                _evenementLayer.AddEvenement(evenement);
                return View(modelView);
            }

            //ViewData["Reponse"] = recurrent;
            //modelView.DateEvent = DateTime.Parse(dateEvenementDebut);
            //if (dateEvenementFin != null)

            //modelView.DateEventFin = DateTime.Parse(dateEvenementFin);

            //if (!(DateTime.TryParse(dateEvenementDebut, out DateTime dateDebutConverti)|| 
            // !(DateTime.TryParse(dateEvenementFin, out DateTime dateFinConverti)||dateDebutConverti>dateFinConverti)))
            //{

            //}
            modelView.SelectListItems = _categorieEvenementLayer.GetTypeEvenements();

            return View(modelView);
        }
    }
}
