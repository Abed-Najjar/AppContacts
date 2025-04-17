using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AppUserDtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required string Password { get; set; }
    }
}
