using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.Utilitaires
{
    public static class UploadFiles
    {

        /// <summary>
        /// Vérifie si le fichier passé en paramètre existe dans le dossier ciblé
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool CheckIfPresent(IFormFile file, string path)
        {

            return File.Exists($"{path}/{file.FileName}");

        }

        /// <summary>
        /// Vérifie si tous les fichiers d'une liste existent dans le répertoire ciblé. Return false si au moins un fichier n'est pas présent
        /// </summary>
        /// <param name="listFile"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool CheckIfPresent(List<IFormFile> listFile, string path)
        {
            foreach (var file in listFile)
            {
                if (!File.Exists($"{path} +/{file.FileName}"))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Upload le fichier cible. Retourne un InfoUpload
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        [HttpPost]
        public static InfoUpload UploadFile(IFormFile postedFile, string path)
        {

            string name = "";
            if (postedFile != null)
            {
                name = path + "/" + postedFile.FileName;
            }

            try
            {
                using (var stream = new FileStream(name, FileMode.Create))
                {
                    postedFile.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Le fichier n'a pas pu être uploadé correctement");
            }



            return new InfoUpload { Count = 1, Done = CheckIfPresent(postedFile, path), Size = postedFile.Length, Paths = new List<string> { name } };
        }


        /// <summary>
        /// Upload une liste de fichiers. Retourne un InfoUpload
        /// </summary>
        /// <param name="listFiles"></param>
        [HttpPost]
        public static InfoUpload UploadFile(List<IFormFile> listFiles, string path)
        {
            InfoUpload iu = new InfoUpload();

            long size = listFiles.Sum(f => f.Length);
            iu.Paths = new List<string>();


            foreach (var file in listFiles)
            {
                if (file.Length > 0)
                {
                    var filePath = path + "/" + file.FileName;

                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }

                        iu.Count++;
                        iu.Paths.Add(filePath);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Le fichier {file.FileName} n'a pas été uploadé correctement.");
                    }
                }
            }

            iu.Size = size;
            iu.Done = CheckIfPresent(listFiles, path);

            return iu;

        }
    }


    public class InfoUpload
    {
        public List<String> Paths { get; set; }
        public long Size { get; set; }
        public int Count { get; set; }
        public bool Done { get; set; }
    }
}
