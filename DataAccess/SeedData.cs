using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ReservationContext(
            serviceProvider.GetRequiredService<DbContextOptions<ReservationContext>>());

        context.Database.EnsureCreated();

        if (context.Rooms.Any()) return;
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
            },
            new Room
            {
                RoomNo = "4",
                Beds = 1,
                Price = 7.99M
            },
            new Room
            {
                RoomNo = "5",
                Beds = 2,
                Price = 7.99M
            },
            new Room
            {
                RoomNo = "6",
                Beds = 3,
                Price = 7.99M
            }
        );
        context.Users.AddRange(
            new User
            {
                FirstName = "test",
                LastName = "test",
                Email = "test@hotel.com",
                IsAdmin = true
            });
        
        context.SaveChanges();
    }
}