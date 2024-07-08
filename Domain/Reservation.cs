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

    [NotMapped] 
    public bool InUse => Start > DateTime.Today
                         && End < DateTime.Today;

    [NotMapped]
    public bool Ended => End >= DateTime.Today;

    [NotMapped]
    public bool CancellableByUser => Start.Subtract(DateTime.Today).Days >= 3;
}