using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrimerWebApiM3.Helpers
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value ==null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string firstLetter = value.ToString()[0].ToString();

            if(firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("Primer Letra debe ir en mayúscula");
            }

            return ValidationResult.Success;
        }
    }
}
