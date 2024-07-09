
namespace Services.Dtos;

public class ReservationDto
{
    public int? Id { get; set; }

    public int RoomId { get; set; }

    public int UserId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }
}