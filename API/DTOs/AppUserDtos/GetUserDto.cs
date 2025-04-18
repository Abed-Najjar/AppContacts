using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AppUserDtos;

public class GetUserDto
{
    [Required]
    public required int Id { get; set; }
    [Required]
    public required string Username { get; set; }
    public string? Email { get; set; }
    [Required]
    public required string Passwordhash { get; set; }
    
}
