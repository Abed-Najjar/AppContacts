using System.ComponentModel.DataAnnotations;

namespace API.DTOs.AppUserDtos;

public class RemoveUserDto
{
    [Required]
    public required int Userid { get; set; }
}
