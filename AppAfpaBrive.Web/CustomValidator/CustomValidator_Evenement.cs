using AppAfpaBrive.Web.ModelView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.CustomValidator
{
    public class CustomValidator_Evenement: ValidationAttribute
    {
        

        public CustomValidator_Evenement()
        {
            
        }
        protected override ValidationResult IsValid(object value,
           ValidationContext validationContext)
        {
            var modelView = (EvenementModelView)validationContext.ObjectInstance;
            value = modelView.DateEvent;

            if (modelView.DateEventFin is not null && modelView.DateEventFin < (DateTime)value )
            {
                return new ValidationResult("La date de début de contrat doit être antérieure à celle de fin");
            }

            return ValidationResult.Success;
        }
    }


}

