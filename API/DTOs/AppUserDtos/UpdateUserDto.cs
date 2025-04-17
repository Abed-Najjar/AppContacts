using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AppUserDtos;

public class UpdateUserDto
{
    [Required]
    public int Id { get; set; }  // The ID of the user being updated

    [StringLength(50, ErrorMessage = "Username can't be longer than 50 characters")]
    public string? Username { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
}
