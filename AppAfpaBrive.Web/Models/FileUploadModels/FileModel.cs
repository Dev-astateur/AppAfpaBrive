using AppAfpaBrive.Web.Models.FileUploadModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Models
{
    public class FilesModel 
    {
        [AllowedExtension(new string[] { ".xls", ".xlsx"}, ErrorMessage ="Format de fichier invalide. Extensions autorisées: .xls ou .xlsx")]
        [Required(ErrorMessage = "Merci de choisir un fichier")]
        [Display(Name = "file")]
        [DataType(DataType.Upload)]
        public IFormFile file { get; set; }
  
    }
}
