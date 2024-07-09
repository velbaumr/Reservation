using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class RoomDto
{
    public int? Id { get; set; }

    public string? RoomNo { get; set; }

    public int Beds { get; set; }

    public decimal Price { get; set; }
}