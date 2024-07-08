namespace Services.Dtos;

public class ReservationViewDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public RoomDto? RoomData { get; set; }
}