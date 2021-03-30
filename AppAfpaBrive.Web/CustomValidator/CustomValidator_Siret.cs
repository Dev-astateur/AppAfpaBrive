﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AppAfpaBrive.Web.CustomValidator
{
    //[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomValidator_SiretAttribute:ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            string siret = value.ToString();
            siret = String.Concat(siret.Where(c => !Char.IsWhiteSpace(c)));
            int total = 0;
            if(siret.StartsWith("356000000"))
            {
                for (int i = 0; i < siret.Length; i++)
                {
                    char y = siret[i];
                    int x = (int)char.GetNumericValue(y);
                    total += x;
                }
                if (total%5 == 0)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            else
            {
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

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        public DateGreaterThanAttribute(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        private string DateToCompareToFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate > earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not later");
            }
        }
    }


}
