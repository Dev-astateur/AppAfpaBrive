using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.Calendar;
using AppAfpaBrive.Web.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.Calendrier
{
    [AllowAnonymous]
    public class EvenementController : Controller
    {
        private Layer_Evenement _layer;

        public EvenementController(AFPANADbContext context)
        {
            _layer = new Layer_Evenement(context);
        }

        [HttpGet]
        public IActionResult Index(int? month, int year)
        {
            EvenementModelView model = new();

            //List<CalendarEvent> events = new List<CalendarEvent>();
            //CalendarEvent event1 = new CalendarEvent()
            //{
            //    Id = 1,
            //    Nom = "Carnaval",
            //    Titre = "Carnaval AFPA",
            //    Date = new DateTime(2017, 11, 01),
            //    Type = "Fete",
            //    Lieu = "Brive"
            //};
            //CalendarEvent event2 = new CalendarEvent()
            //{
            //    Id = 2,
            //    Nom = "Noel",
            //    Titre = "Noel AFPA",
            //    Date = new DateTime(2017, 11, 02),
            //    Type = "Noel",
            //    Lieu = "Brive"
            //};
            //events.Add(event1);
            //events.Add(event2);
            if (month is null)
                month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            model.Month = (int)month;
            model.Year = year;
            var events = _layer.GetEvenements(month, year);
            model.CalendarEvents = events;
            return View(model);

        }

        
        [HttpGet]
        public IActionResult Precedent(string precedent, int year)
         {
            EvenementModelView model = new();
            int mois = DateTime.ParseExact(precedent, "MMMM", CultureInfo.CurrentCulture).Month;
            DateTime timeT = new DateTime(year, mois, 1);
            DateTime time = timeT.AddMonths(-1);
           
            var events = _layer.GetEvenements(time.Month, time.Year);
            model.Month = time.Month;
            model.Year = time.Year;
            model.CalendarEvents = events;
            return View(model);

        }

        
        [HttpGet]
        public IActionResult Suivant(string suivant, int year)
        {

            EvenementModelView model = new();
            int mois = DateTime.ParseExact(suivant, "MMMM", CultureInfo.CurrentCulture).Month;
            DateTime timeT = new DateTime(year, mois, 1);
            DateTime time = timeT.AddMonths(1);

            var events = _layer.GetEvenements(time.Month, time.Year);

            model.Month = time.Month;
            model.Year = time.Year;
            model.CalendarEvents = events;
            return View(model);
        }
        public IActionResult DetailsEvenements (int id)
        {
            EvenementModelView model = new ();
            Evenement evenement = _layer.GetEventById(id);
            model.Titre = evenement.Titre;
            model.Heure = evenement.Heure;
            model.DateEvent = evenement.DateEvent;
            model.DétailsEvent = evenement.DétailsEvent;
            

            return View(model);
        }



    }
}
