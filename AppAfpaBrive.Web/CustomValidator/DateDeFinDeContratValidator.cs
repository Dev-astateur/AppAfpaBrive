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
    public class DateDeFinDeContratValidator: ValidationAttribute
    {
        public DateDeFinDeContratValidator()
        {
            
        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var contratModelView = (ContratModelView)validationContext.ObjectInstance;
            var dateFinDeContrat = ((DateTime?)value);

            if (dateFinDeContrat < DateTime.Now.AddYears(-20) || dateFinDeContrat > DateTime.Now.AddYears(20))
            {
                return new ValidationResult("La date saisie est hors limites.");
            }
            else if (dateFinDeContrat is not null && contratModelView.DateEntreeFonction >= dateFinDeContrat)
            {
                return new ValidationResult("La date de fin de contrat doit être postérieure à celle de début de contrat.");
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}
