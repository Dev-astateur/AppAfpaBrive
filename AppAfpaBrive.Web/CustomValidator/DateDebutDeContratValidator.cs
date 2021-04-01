using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.Web.ModelView;


namespace AppAfpaBrive.Web.CustomValidator
{
    public class DateDebutDePeeValidator : ValidationAttribute
    {
        public DateDebutDePeeValidator()
        {

        }

        public string GetErrorMessage() =>
            $"La date de début de contrat doit être antérieure à celle de fin de contrat.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var contratModelView = (Date_ModelView)validationContext.ObjectInstance;
            var dateDebutDeContrat = ((DateTime)value);

            if (contratModelView.Date2 <= dateDebutDeContrat)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }

    public class DateDeFinDePeeValidator : ValidationAttribute
    {
        public DateDeFinDePeeValidator()
        {

        }

        public string GetErrorMessage() =>
            $"La date de fin de contrat doit être postérieure à celle de début de contrat.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var contratModelView = (Date_ModelView)validationContext.ObjectInstance;
            var dateFinDeContrat = ((DateTime)value);

            if (contratModelView.Date1 >= dateFinDeContrat)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }

}
