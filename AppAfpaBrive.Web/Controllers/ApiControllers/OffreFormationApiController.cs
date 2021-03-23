﻿using AppAfpaBrive.DAL;
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
                ProduitDeFormationLayer produitLayer = new ProduitDeFormationLayer(db);
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