using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Layers;
using AppAfpaBrive.DAL; 

namespace AppAfpaBrive.Web.Controllers
{
    [Route("api/appelationsRome")]
    [ApiController]
    public class AppellationsRomeApiController : ControllerBase
    {
        private Layer_AppelationRomes _AppelationRomes = null;

        public AppellationsRomeApiController(AFPANADbContext context)
        {
            _AppelationRomes = new Layer_AppelationRomes(context); 
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var libellesAppelations = _AppelationRomes.SearchLibellesAppellationsRome(term); //.SearchApellationsRome(term).Select(x => new { CodeAppellation = x.CodeAppelationRome, Libelle = x.LibelleAppelationRome });   
                return Ok(libellesAppelations);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
