using System;
using System.ComponentModel.DataAnnotations;

namespace SampleTaskApp.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaxMinAgeAttribute : ValidationAttribute
    {
        public int MaxAge { get; set; } = int.MaxValue; // Optional parameter for maximum age
        public int MinAge { get; set; } = int.MinValue; // Optional parameter for minimum age

        public MaxMinAgeAttribute(int maxAge = int.MaxValue, int minAge = int.MinValue)
        {
            MaxAge = maxAge;
            MinAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Allow null values if not required
            }

            if (int.TryParse(value.ToString(), out int age))
            {
                // Case 1: Only MinAge is set (MaxAge is 0 or not provided)
                if (MinAge > 0 && MaxAge == int.MaxValue && age >= MinAge)
                {
                    return ValidationResult.Success;
                }

                // Case 2: Only MaxAge is set (MinAge is 0 or not provided)
                if (MaxAge > 0 && MinAge == int.MinValue && age <= MaxAge)
                {
                    return ValidationResult.Success;
                }

                // Case 3: Both MinAge and MaxAge are set
                if (age >= MinAge && age <= MaxAge)
                {
                    return ValidationResult.Success;
                }

                // Case 4: Invalid range
                return new ValidationResult(GetErrorMessage());
            }

            return new ValidationResult("Invalid age format.");
        }

        private string GetErrorMessage()
        {
            if (MinAge > int.MinValue && MaxAge < int.MaxValue)
                return $"Age must be between {MinAge} and {MaxAge}.";
            if (MinAge > int.MinValue)
                return $"Age must be greater than or equal to {MinAge}.";
            if (MaxAge < int.MaxValue)
                return $"Age must be less than or equal to {MaxAge}.";

            return "Invalid age range.";
        }
    }
}
