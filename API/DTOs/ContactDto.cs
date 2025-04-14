using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class ContactDto
{
    [Required]
    [MaxLength(10)]
    public required string Username { get; set; }
    public string? Email { get; set; }
}
