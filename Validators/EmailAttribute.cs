using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SampleTaskApp.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow null values if the field is not required
            }

            var email = value.ToString();

            // Regular expression for email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (Regex.IsMatch(email, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid email address format.");
        }
    }
}
