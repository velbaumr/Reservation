using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Reservation : Entity
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Room? Room { get; set; }

    public int RoomId { get; set; }
    public User? User { get; set; }

    public int UserId { get; set; }

    public bool Cancelled { get; set; }

    [NotMapped] public bool InUse => Start.Date <= Today && End.Date >= Today;

    [NotMapped] public DateTime Today { get; set; } = DateTime.Today;

    [NotMapped] public bool Ended => End.Date <= Today;

    [NotMapped] public bool CancellableByUser => Start.Subtract(Today).Days >= 3;
}