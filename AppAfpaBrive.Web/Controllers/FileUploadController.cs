using AppAfpaBrive.Web.Models.FileUploadModels;
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
        protected string path = "";


        public FileUploadController()
        {
            path = "./Data/Documents";
        }

        public IActionResult Index()
        {
            return View();
        }


      
        [HttpPost]
        public async Task<IActionResult> Index(FileModel file)
        {
            var filePath = path + "/" + file.uploaded.FileName;


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.uploaded.CopyToAsync(stream);
            }

            if (ModelState.IsValid)
            {
                return Ok(new { file.uploaded.Length, filePath });
            }

            return View();
        }
    }
}
