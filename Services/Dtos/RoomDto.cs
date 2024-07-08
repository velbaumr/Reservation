using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class RoomDto
{
    public int? Id { get; set; }
    
    [Required]
    public string? RoomNo { get; set; }
    
    [Required]
    public int Beds { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}