﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class EntreprisesController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
