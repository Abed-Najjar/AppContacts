using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ContactsDtos;

public class UpdateContactDto
{
    [Required]
    [MaxLength(10)]
    public required string Username { get; set; }
    public string? Email { get; set; }
}
