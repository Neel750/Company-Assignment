using System;
using System.ComponentModel.DataAnnotations;
 
namespace SCFA.Models
{
    public class BDayCheck : ValidationAttribute
    {
        private DateTime date = DateTime.Now;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime)
                {
                    if (Convert.ToDateTime(value) < date)
                    {
                        return new ValidationResult("You have entered todays date.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}