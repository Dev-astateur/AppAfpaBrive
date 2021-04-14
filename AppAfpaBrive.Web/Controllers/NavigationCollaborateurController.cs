using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    [Authorize(Roles = "Formateur,CollaborateurAFPA,Administrateur")]
    public class NavigationCollaborateurController : Controller
    {
        private readonly Layer_CollaborateurAfpa _collaborateurAfpaLayer = null;

        public NavigationCollaborateurController( AFPANADbContext context )
        {
            _collaborateurAfpaLayer = new Layer_CollaborateurAfpa(context);
        }

        public IActionResult Index()
        {
            string id = HttpContext.User.Identity.Name;
            
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(Index),nameof(AccueilController));

            return View(_collaborateurAfpaLayer.GetCollaborateurById(id));
        }
    }
}
