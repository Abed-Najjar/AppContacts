using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AppUserDtos;

public class RemoveUserDto
{
    [Required(ErrorMessage = "Id is required")]
    public required int Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; set; }
}
