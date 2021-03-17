using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Models.FileUploadModels
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {

        private readonly string[] _extension;

        public AllowedExtensionAttribute(string[] extensions)
        {
            _extension = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extension.Contains(extension.ToLower()))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

    }
}
