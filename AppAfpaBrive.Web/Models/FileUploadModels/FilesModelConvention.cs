using AppAfpaBrive.Web.Models.FileUploadModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.Models
{
    public class FilesModelConvention
    {
        [AllowedExtension(new string[] { ".pdf", ".odt" }, ErrorMessage = "Format de fichier invalide. Extensions autorisées: .pdf ou .odt")]
        [Display(Name = "file")]
        [DataType(DataType.Upload)]
        public List<IFormFile> file { get; set; }

    }

}
