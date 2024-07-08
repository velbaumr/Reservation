using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ReservationContext : DbContext
{
    public ReservationContext()
    {
    }
    
    public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .HasMany(c => c.Reservations)
            .WithOne(e => e.Room);
        
        modelBuilder.Entity<User>()
            .HasMany(c => c.Reservations)
            .WithOne(e => e.User);
    }
    
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
}