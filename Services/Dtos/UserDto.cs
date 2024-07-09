﻿using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class UserDto
{
    public int? Id { get; set; }

    [Required] public string? FirstName { get; set; }

    [Required] public string? LastName { get; set; }

    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] public string? IdCode { get; set; }
    
    public bool IsAdmin { get; set; }
}