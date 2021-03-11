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

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile)
            {
                return ValidateSingleFile((IFormFile)value);
            }
            else { }
        }

        private ValidationResult ValidateSingleFile(IFormFile value)
        {
            throw new NotImplementedException();
        }
    }
}
