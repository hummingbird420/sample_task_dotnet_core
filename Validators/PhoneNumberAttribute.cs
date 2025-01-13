using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SampleTaskApp.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow null values if the field is not required
            }

            var phoneNumber = value.ToString();

            // Regular expression for Bangladeshi phone numbers
            string pattern = @"^(\+8801[3-9]\d{8}|01[3-9]\d{8})$";

            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid Bangladeshi phone number. Valid formats are +8801XXXXXXXX or 01XXXXXXXX.");
        }
    }
}

