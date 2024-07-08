using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Room : Entity
{
    [Required] public string? RoomNo { get; set; }
    [Required] public int Beds { get; set; }
    [Required] public decimal Price { get; set; }
    public ICollection<Reservation>? Reservations { get; init; } = new List<Reservation>();
}