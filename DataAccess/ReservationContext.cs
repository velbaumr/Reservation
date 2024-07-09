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

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>()
            .HasMany(c => c.Reservations)
            .WithOne(e => e.Room)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(c => c.Reservations)
            .WithOne(e => e.User)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => new {u.Email, u.IdCode})
            .IsUnique();
            
    }
}