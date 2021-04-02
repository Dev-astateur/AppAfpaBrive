using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.Calendar;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Calendrier
{
    public class EvenementFormateurController : Controller
    {
        private Layer_CategorieEvenement _categorieEvenementLayer;
        private Layer_Evenement _evenementLayer;
        public EvenementFormateurController(AFPANADbContext context)
        {
            _categorieEvenementLayer = new Layer_CategorieEvenement(context);
            _evenementLayer = new Layer_Evenement(context);
        }

        public IActionResult Create()
        {
            EvenementModelView modelView = new EvenementModelView();
            modelView.SelectListItems = _categorieEvenementLayer.GetTypeEvenements();
            modelView.DateEvent = DateTime.Now;
           
            return View(modelView);
        }
        [HttpPost]
        public IActionResult Create(EvenementModelView modelView, string recurrent, string dateEvenementDebut, string dateEvenementFin)
        
        {
           // EvenementModelView modelView = new EvenementModelView();
            modelView.SelectListItems = _categorieEvenementLayer.GetTypeEvenements();
            ViewData["Reponse"] = recurrent;
            modelView.DateEvent = DateTime.Parse(dateEvenementDebut);
            if (dateEvenementFin != null)

                modelView.DateEventFin = DateTime.Parse(dateEvenementFin);

            //if (!(DateTime.TryParse(dateEvenementDebut, out DateTime dateDebutConverti)|| 
            // !(DateTime.TryParse(dateEvenementFin, out DateTime dateFinConverti)||dateDebutConverti>dateFinConverti)))
            //{

            //}
            return View(modelView);
        }
    }
}
