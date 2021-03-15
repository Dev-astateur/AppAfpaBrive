using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Models.FileUploadModels
{
    public class FileModel
    {
        [FileAttribute]
        [DataType(DataType.Upload)]
        public IFormFileWrapper Uploaded { get; set; }
       
        public class IFormFileWrapper
        {
           
            public IFormFile File{ set; get; }
        }
    }
}
