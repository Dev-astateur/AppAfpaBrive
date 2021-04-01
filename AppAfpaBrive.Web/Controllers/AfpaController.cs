using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class AfpaController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Connexion";
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
