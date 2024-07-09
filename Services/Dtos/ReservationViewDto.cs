using System.ComponentModel.DataAnnotations;

namespace Services.Dtos;

public class ReservationViewDto
{
    [Required] public int Id { get; set; }

    [Required] public int UserId { get; set; }

    [Required] public DateTime From { get; set; }

    [Required] public DateTime To { get; set; }

    [Required] public RoomDto? RoomData { get; set; }
}