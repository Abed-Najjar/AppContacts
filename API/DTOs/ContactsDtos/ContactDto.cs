using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ContactsDtos;

public class ContactDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(10)]
    public required string Username { get; set; }
    public string? Email { get; set; }
}
