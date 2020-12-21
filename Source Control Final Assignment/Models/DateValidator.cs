//Server Side Custom Validation.
//It will check if user has mistakelly entered today's date as birthdate.
using System;
using System.ComponentModel.DataAnnotations;
 
namespace SCFA.Models
{
    public class BDayCheck : ValidationAttribute
    {
        private readonly DateTime date = DateTime.Now;//Get Today's date.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime)
                {
                    if (Convert.ToDateTime(value) == date)
                    {
                        return new ValidationResult("You have entered today's date.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}