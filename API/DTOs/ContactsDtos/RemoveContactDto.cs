using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ContactsDtos;

public class RemoveContactDto
{
    [Required]
    public required int Id { get; set; }
    [Required]
    public required string Username { get; set; }
}
