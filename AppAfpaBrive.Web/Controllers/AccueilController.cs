using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    [AllowAnonymous]
    public class AccueilController : Controller
    {
        private readonly IConfiguration _configuration = null;
        private readonly EtablissementLayer _etablissementLayer = null;

        public AccueilController(AFPANADbContext dbContext , IConfiguration configuration )
        {
            _configuration = configuration;
            _etablissementLayer = new EtablissementLayer(dbContext);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var accueilModelView = await _etablissementLayer.GetEtablissementByIdAsync(_configuration.GetSection("Etablissement").Value);
            return View(accueilModelView);
        }
    }
}
