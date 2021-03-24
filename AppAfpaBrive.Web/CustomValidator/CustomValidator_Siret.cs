using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web.CustomValidator
{
    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomValidator_SiretAttribute:ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            string siret = value.ToString();
            int total = 0;
            for (int i = 0; i < siret.Length; i++)
            {
                char y = siret[i];
                int x = (int)char.GetNumericValue(y);
                if (i % 2 == 0)
                {
                    x = x * 2;
                }
                if (x > 9)
                {
                    string o = x.ToString();
                    total += (int)char.GetNumericValue(o[1]) + (int)char.GetNumericValue(o[0]);
                }
                else
                {
                    total += x;
                }
            }
            if (total % 10 == 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}