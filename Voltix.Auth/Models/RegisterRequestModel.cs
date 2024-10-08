using System.ComponentModel.DataAnnotations;

namespace Voltix.Auth.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email must be between {2} and {1} characters", MinimumLength = 3)]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100, ErrorMessage = "Email address cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be between {2} and {1} characters long", MinimumLength = 8)]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; }
    }
}

