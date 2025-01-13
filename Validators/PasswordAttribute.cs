using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SampleTaskApp.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordAttribute : ValidationAttribute
    {
        public int MinLength { get; set; } = 8; // Default minimum length
        public int MaxLength { get; set; } = 100; // Default maximum length
        public bool RequireUppercase { get; set; } = true; // Require at least one uppercase letter
        public bool RequireLowercase { get; set; } = true; // Require at least one lowercase letter
        public bool RequireDigit { get; set; } = true; // Require at least one digit
        public bool RequireSpecialCharacter { get; set; } = true; // Require at least one special character

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow null values if the field is not required
            }

            var password = value.ToString();

            if (password.Length < MinLength || password.Length > MaxLength)
            {
                return new ValidationResult($"Password must be between {MinLength} and {MaxLength} characters.");
            }

            if (RequireUppercase && !Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (RequireLowercase && !Regex.IsMatch(password, @"[a-z]"))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.");
            }

            if (RequireDigit && !Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult("Password must contain at least one digit.");
            }

            if (RequireSpecialCharacter && !Regex.IsMatch(password, @"[\W_]"))
            {
                return new ValidationResult("Password must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}
