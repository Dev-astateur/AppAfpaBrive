using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AutoCompleteOffreFormationApiController : ControllerBase
    {
        private readonly AFPANADbContext _db;

        public AutoCompleteOffreFormationApiController(AFPANADbContext db)
        {
            _db = db;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
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
        [Produces("application/json")]
        [HttpGet("searchPays")]
        public IActionResult SearchPays()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = _db.Pays.Where(p => p.LibellePays.StartsWith(term)).Select(x=>x.LibellePays).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
