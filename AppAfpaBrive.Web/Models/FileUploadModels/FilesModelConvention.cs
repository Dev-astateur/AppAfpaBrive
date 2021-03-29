using AppAfpaBrive.Web.Models.FileUploadModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.Models
{
    public class FilesModelConvention
    {
        [AllowedExtension(new string[] { ".pdf" }, ErrorMessage = "Format de fichier invalide. Extensions autorisées: .pdf")]
        [Required(ErrorMessage = "Merci de choisir un fichier")]
        [Display(Name = "file")]
        [DataType(DataType.Upload)]
        public IFormFile file { get; set; }

    }

}
