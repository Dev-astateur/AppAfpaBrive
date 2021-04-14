
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
using AppAfpaBrive.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using AppAfpaBrive.Web.Areas.Identity.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;

namespace AppAfpaBrive.Web.Controllers
{
    [Authorize(Roles = "Administrateur,CollaborateurAFPA")]
    public class FileUploadController : Controller
    {

        private readonly AFPANADbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<AppAfpaBriveUser> _userManager;
        private readonly IFileProvider _fileProvider;
        protected string Path { get; set; }

        public FileUploadController(AFPANADbContext context, IConfiguration config, UserManager<AppAfpaBriveUser> userManager, IFileProvider fileProvider)
        {
       
            _context = context;
            _config = config;
            _userManager = userManager;
            _fileProvider = fileProvider;
            Path = fileProvider.GetFileInfo(_config.GetSection("RepertoireImportData").Value).PhysicalPath;
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
                    var response = UploadFiles.UploadFile(postedFile, Path);

                    if (response.Done)
                    {
                        Utilitaires.IntegrationExcelOffre integration = new Utilitaires.IntegrationExcelOffre(_config, _context,_userManager);
                        string pathFile = response.Paths.First();

                        integration.IntegrerDonnees(uploadFile.MatriculeCollaborateurAfpa, uploadFile.CodeProduitFormation, pathFile);

                        Response.WriteAsync("<script>alert('Réussi!')</script>");

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
