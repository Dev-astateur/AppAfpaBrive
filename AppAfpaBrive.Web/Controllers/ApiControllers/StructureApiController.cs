using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers.AnnuaireSocialLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers.ApiControllers
{
    [Route("api/structures")]
    [ApiController]
    public class StructureApiController : ControllerBase
    {


        private readonly AFPANADbContext db;

        public StructureApiController (AFPANADbContext DB)
        {
            db = DB;
        }

        [Produces("application/json")]
        [HttpGet("getFormateur")]
        public async Task<IActionResult> GetStructureNameStartWith()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                StructureLayer ofl = new StructureLayer(db);
                var names;

                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
