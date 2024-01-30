using System.ComponentModel.DataAnnotations;

namespace WebApplication8.View_Model
{
    public class RegisterVM
    {

        [Required(ErrorMessage = "name is required.")]
        [MinLength(10, ErrorMessage = "name must be at least 10 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
