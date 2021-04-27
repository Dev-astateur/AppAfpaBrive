﻿using AppAfpaBrive.DAL;
using System;
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
            if(value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            string siret = value.ToString();
            siret = String.Concat(siret.Where(c => !Char.IsWhiteSpace(c)));
            int total = 0;
            // Poste cas particulier
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

    public class CustomValidator_Pays : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            AFPANADbContext _db = new AFPANADbContext();
            string libelle = value.ToString();
            var pays = _db.Pays.Where(x => x.LibellePays == libelle).Select(x=>x.Idpays2).FirstOrDefault();
            if(pays == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

}
