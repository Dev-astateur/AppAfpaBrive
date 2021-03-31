using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class NavigationStagiaireController : Controller
    {
        /// <summary>
        /// action qui va afficher dans une seule fenetre le menu pour le bénéficiare
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string matricule)
        {
            if (string.IsNullOrWhiteSpace(matricule))
                return NotFound();
             

            return View();
        }
    }
}
