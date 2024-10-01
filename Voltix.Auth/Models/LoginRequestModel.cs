using System.ComponentModel.DataAnnotations;

namespace Voltix.Auth.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(254, ErrorMessage = "Email is too long")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100, ErrorMessage = "Password is too long")]
        public string Password { get; set; }
    }
}