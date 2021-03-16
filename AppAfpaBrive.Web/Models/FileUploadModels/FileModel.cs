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

        public int Size { get; set;}

        [AllowedExtension(new string[] { ".xls", ".xlsx"}, ErrorMessage ="Format de fichier invalide")]
        [Required(ErrorMessage = "Please select a file.")]
        [Display(Name = "file")]
        [DataType(DataType.Upload)]
        public IFormFile file { get; set; }
        
    }
}
