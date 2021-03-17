﻿using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoCompleteOffreFormationApiController : ControllerBase
    {
        private AFPANADbContext _db;

        public AutoCompleteOffreFormationApiController(AFPANADbContext db)
        {
            _db = db;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _db.ProduitFormations.Where(p => p.LibelleProduitFormation.Contains(term)).Select(o => o.LibelleProduitFormation).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
