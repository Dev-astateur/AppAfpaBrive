using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.Layers; 

namespace AppAfpaBrive.Web.CustomValidator
{
    public class LibelleFonctionContratValidator:ValidationAttribute
    {
        public LibelleFonctionContratValidator()
        {

        }
        
        public string GetErrorMessage() =>
            $"Veuillez selectionner un intitulé de poste parmi les suggestions" ;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AFPANADbContext _db = new AFPANADbContext();
            List<string> Libelles = _db.AppelationRomes.Select(ar => ar.LibelleAppelationRome).ToList();

            var userInput = value;

            if (!Libelles.Contains(userInput))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
