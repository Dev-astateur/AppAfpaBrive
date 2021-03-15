using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Models.FileUploadModels
{
    public class FileAttribute : ValidationAttribute
    {
        public string[] FileTypes { get; set; }

        /// <summary>
        /// A supprimer et remplacer par un fichier config
        /// </summary>
        public FileAttribute()
        {
            FileTypes = new string[] {"xls", "xlsx" };
        }
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (value is null)
            {
                return new ValidationResult("Merci de choisir un fichier valide");
            }
            else 
            {
                
                string extension = System.IO.Path.GetExtension(file.Uploaded.File.FileName);
                if(!FileTypes.Contains(extension))
                {
                    return new ValidationResult($"L'extension {extension} n'est pas prise en charge");
                }

                if(file.Uploaded.File.Length == 0)
                {
                    return new ValidationResult("Le fichier ne doit pas être vide");
                }
                return ValidationResult.Success;
            }

        }

       
    }
}
