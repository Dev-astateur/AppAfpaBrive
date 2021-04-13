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
        [HttpGet("getByName")]
        public IActionResult GetStructureNameStartWith()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                StructureLayer sl = new StructureLayer(db);
                var names = sl.GetStructuresStartWith(term);

                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }


        [Produces("application/json")]
        [HttpGet("getByLocation")]
        public IActionResult GetStructureByLocation()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                StructureLayer sl = new StructureLayer(db);
                var names = sl.GetStructuresByLocation(term);

                return Ok(names);

            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
