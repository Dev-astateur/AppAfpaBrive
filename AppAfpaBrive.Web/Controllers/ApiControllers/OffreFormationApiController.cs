using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.ApiControllers
{
    [Route("api/offreFormation")]
    [ApiController]
    public class OffreFormationApiController : Controller
    {

        private AFPANADbContext db = new AFPANADbContext();


        [Produces("application/json")]
        [HttpGet("getOffre")]
        public async Task<IActionResult> GetOffre()
        {
            try
            {
                ProduitFormationLayer produitLayer = new ProduitFormationLayer(db);
                string term = HttpContext.Request.Query["term"].ToString();
                var offre = produitLayer.GetProduitFormationStartWith(term).Select(x => new { Id = x.CodeProduitFormation, Libelle = x.LibelleProduitFormation });

                var offres = db.ProduitFormations.Where(x => x.CodeProduitFormation.ToString().StartsWith(term)).Select(x => new { Id = x.CodeProduitFormation, Libelle = x.LibelleProduitFormation }).ToList();
                return Ok(offres);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
