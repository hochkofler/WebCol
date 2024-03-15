using System.ComponentModel.DataAnnotations;
using System;

namespace WebCol.Utilities
{
    public class DateNoFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime)
            {
                DateTime fechaIngresada = (DateTime)value;
                if (fechaIngresada.Date > DateTime.Now.Date)
                {
                    return new ValidationResult("La fecha no puede ser posterior a la fecha actual.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
