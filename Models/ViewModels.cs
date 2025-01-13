using SampleTaskApp.Validators;

namespace SampleTaskApp.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [Password(MinLength = 8, MaxLength = 16, RequireUppercase = true, RequireLowercase = true, RequireDigit = true, RequireSpecialCharacter = true,
            ErrorMessage = "Password does not meet the required criteria.")]
        public string Password { get; set; }
    }

    public class TokenViewModel
    {
        public string Token { get; set; }
    }
}
