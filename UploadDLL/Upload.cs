using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace UploadDLL
{
    public class Upload
    {

        [HttpPost]
        public async Task<IActionResult> Index(FileModel postedFile)
        {

            if (ModelState.IsValid)
            {
                var filePath = path + "/" + postedFile.Uploaded.File.FileName;


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.Uploaded.File.CopyToAsync(stream);
                }

                if (ModelState.IsValid)
                {
                    return Ok(new { postedFile.Uploaded.File.Length, filePath });
                }
            }


            return View();
        }

    }
}
