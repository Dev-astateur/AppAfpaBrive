using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;
namespace AppAfpaBrive.Web.CustomValidator
{
    public class DateDebutDeContratValidator : ValidationAttribute
    {
        public DateDebutDeContratValidator()
        {

        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var contratModelView = (ContratModelView)validationContext.ObjectInstance;
            var dateDebutDeContrat = ((DateTime)value);

            if (dateDebutDeContrat < DateTime.Now.AddYears(-20) || dateDebutDeContrat > DateTime.Now.AddYears(20))
            {
                return new ValidationResult("La date saisie est hors limites.");
            }
            else if (contratModelView.DateSortieFonction is not null && contratModelView.DateSortieFonction <= dateDebutDeContrat)
            {
                return new ValidationResult("La date de début de contrat doit être antérieure à celle de fin de contrat.");
            }
            else
            {
                return ValidationResult.Success;
            }
            
        }
    }
}
