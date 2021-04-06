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
            var files = value as List<IFormFile>;
            if(files != null)
            {
                foreach (var item in files)
                {
                    if (item != null)
                    {
                        var extension = Path.GetExtension(item.FileName);
                        if (!_extension.Contains(extension.ToLower()))
                        {
                            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
