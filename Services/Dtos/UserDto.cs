using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class UserDto
{
    public int? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [EmailAddress] public string? Email { get; set; }

    public string? IdCode { get; set; }
    
    public bool IsAdmin { get; set; }
}