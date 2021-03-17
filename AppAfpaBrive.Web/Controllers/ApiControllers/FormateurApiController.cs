using AppAfpaBrive.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.ApiControllers
{
    [Route("api/formateur")]
    [ApiController]
    public class FormateurApiController : ControllerBase
    {

        private AFPANADbContext db = new AFPANADbContext();

        [Produces("application/json")]
        [HttpGet("getFormateur")]
        public async Task<IActionResult> GetFormateur()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                Debug.WriteLine(term);
                var names = db.CollaborateurAfpas.Where(p => p.NomCollaborateur.StartsWith(term)).Select(x => new { Nom = x.NomCollaborateur, Id = x.MatriculeCollaborateurAfpa });
                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
