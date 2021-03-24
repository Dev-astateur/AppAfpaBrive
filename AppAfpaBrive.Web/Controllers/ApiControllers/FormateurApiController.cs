using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers;
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

        private readonly AFPANADbContext db;

        public FormateurApiController(AFPANADbContext DB)
        {
            db = DB;
        }


        /// <summary>
        /// Retourne les formateurs par le début de leurs noms
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("getFormateur")]
        public async Task<IActionResult> GetFormateurStartWith()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                CollaborateurAfpaLayer ofl = new CollaborateurAfpaLayer(db);
                Debug.WriteLine(term);
                var names = ofl.GetCollaborateurStartWith(term).Select(x => new { Nom = x.NomCollaborateur, Id = x.MatriculeCollaborateurAfpa });
              
                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
