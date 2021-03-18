
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.Web.Utilitaires;
using AppAfpaBrive.Web.Models;
using AppAfpaBrive.Web.ViewModels.IntegrationExcelOffre;

namespace AppAfpaBrive.Web.Controllers
{
    public class FileUploadController : Controller
    {


        protected string Path { get; set; }
      
        public FileUploadController()
        {
            ///A enlever
            Path = "./Data/Documents";
          
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IntegrerOffre(IntegrationExcelOffreCreate uploadFile)
        {

            if (ModelState.IsValid)
            {
                var postedFile = uploadFile.fileModel.file;
                try
                {
                    var Response = UploadFiles.UploadFile(postedFile, Path);

                    if (Response.Done)
                    {
                        return View(viewName: "Index");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                catch (Exception e)
                {
                    Response.WriteAsync("<script>alert('" + e + "')</script>");
                    return View(viewName: "Index");
                }
            }

            return View(viewName: "Index");

        }
    }
}
