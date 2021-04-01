using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Views.EditerInfosStagiaire
{
    [Route("api/chargerlibelleformation")]
    [ApiController]
    public class ChargerLibelleFormationController : ControllerBase
    {

        private AFPANADbContext _context;

        public ChargerLibelleFormationController(AFPANADbContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var libelles = _context.OffreFormations.Where(p => p.LibelleOffreFormation.Contains(term)).Select(p => p.LibelleOffreFormation).ToList();
                return Ok(libelles);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
