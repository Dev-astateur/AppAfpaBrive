using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Controllers
{
    public class FileUploadController : Controller
    {
       

        [HttpPost("FileUpload")]
        public async Task<IActionResult> Index(IFormFile file, string path)
        {
            var filePath = path;
            if (file.Length > 0)
            {
                
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok(new { file.Length, filePath});
        }
    }
}
