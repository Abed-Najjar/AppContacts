using System;

namespace API.Entities;

public class Contacts
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public string? Email { get; set; }
}
