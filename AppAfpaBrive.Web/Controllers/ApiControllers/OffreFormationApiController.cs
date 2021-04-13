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
        private readonly AFPANADbContext db = new AFPANADbContext();

        [Produces("application/json")]
        [HttpGet("getOffre")]
        public IActionResult GetOffre()
        {
            try
            {
                Layer_ProduitDeFormation produitLayer = new Layer_ProduitDeFormation(db);
                string term = HttpContext.Request.Query["term"].ToString();
                var offre = produitLayer.GetProduitFormationStartWith(term).Select(x => new { Id = x.CodeProduitFormation, Libelle = x.LibelleProduitFormation });

               
                return Ok(offre);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
