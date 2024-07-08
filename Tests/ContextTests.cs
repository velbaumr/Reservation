using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class ContextTests
{
    private readonly DbContextOptions<ReservationContext> _dbContextOptions = new DbContextOptionsBuilder<ReservationContext>()
        .UseInMemoryDatabase(databaseName: "TestDb1")
        .Options;

    public ContextTests()
    {
        using var context = new ReservationContext(_dbContextOptions);
        SeedTestData(context);
    }

    [Fact]
    public void CreatesContext()
    {
        using var context = new ReservationContext(_dbContextOptions);
        Assert.NotNull(context);
    }

    [Fact]
    public async Task GetsRooms()
    {
        await using var context = new ReservationContext(_dbContextOptions);
        var result = await context.Rooms.ToListAsync();
        
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task AddsReservation()
    {
        await using var context = new ReservationContext(_dbContextOptions);
        
        await context.Reservations.AddAsync(
                           new Reservation
                           {
                               User = await context.Users.FindAsync(1),
                               Room = await context.Rooms.FindAsync(2),
                               Start = DateTime.Now + TimeSpan.FromDays(0),
                               End = DateTime.Now + TimeSpan.FromDays(6)
                           });
        await context.SaveChangesAsync();
        Assert.Single(context.Reservations);
    }

    private void SeedTestData(ReservationContext context)
    {
        context.Rooms.AddRange(
            new Room
            {
                RoomNo = "1",
                Beds = 1,
                Price = 7.99M
            },
            new Room
            {
                RoomNo = "2",
                Beds = 2,
                Price = 7.99M
            },
            new Room
            {
                RoomNo = "3",
                Beds = 3,
                Price = 7.99M
            }
            );
        context.Users.AddRange(
            new User
            {
                FirstName = "test",
                LastName = "test",
                Email = "raul@kalamaja.com"
            });
        context.SaveChanges();
    }
}