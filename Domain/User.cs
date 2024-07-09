namespace Domain;

public class User : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }

    public string? IdCode { get; set; }

    public bool IsAdmin { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
}