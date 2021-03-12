using AppAfpaBrive.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.DAL;

namespace AppAfpaBrive.Web.Controllers.Convention
{
    public class ConventionController : Controller
    {
        

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Create(int? id)
        {
            ViewData["Message"] = "Your application description page " + id + " !";

            return View();
        }
    }
}
