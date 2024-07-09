using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services;

public class ReservationService(
    ReservationContext context, IUserService userService, IRoomService roomService) : IReservationService
{
    public async Task<IEnumerable<ReservationViewDto?>> GetValidReservations()
    {
        var reservations = await context.Reservations.Include(x => x.Room)
            .Where(x =>
                x.Start.Date >= DateTime.Today &&
                !x.Cancelled)
            .ToListAsync();

        return reservations.Select(Map);
    }

    public async Task<IEnumerable<ReservationViewDto?>> GetReservationsForUser(int userId)
    {
        var reservations = await context.Reservations.Include(x => x.Room)
            .Where(x => x.UserId == userId && !x.Cancelled)
            .ToListAsync();

        var filtered = reservations.Where(x => x.CancellableByUser);
        return filtered.Select(Map);
    }

    public async Task CancelReservation(int reservationId, int userId)
    {
        var user = await userService.GetUser(userId);
        if (user == null) throw new ArgumentException("wrong user id");
        
        var reservation = await context.Reservations.FindAsync(reservationId);
        if (reservation == null) throw new ArgumentException("wrong reservation id");
        
        if (user.IsAdmin && !reservation.Ended)
        {
            reservation.Cancelled = true;
        }
        else
        {
            reservation.Cancelled = reservation.CancellableByUser;
        }

        await context.SaveChangesAsync();
    }


    public async Task<bool> UpdateReservation(ReservationDto reservation)
    {
        var toUpdate = await context.Reservations.FindAsync(reservation.Id);

        if (toUpdate == null)
            return false;
        toUpdate.Start = reservation.From;
        toUpdate.End = reservation.To;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddReservation(ReservationDto reservation)
    {
        var toAdd = new Reservation
        {
            Start = reservation.From,
            End = reservation.To,
            RoomId = reservation.RoomId,
            UserId = reservation.UserId
        };

        var available = await roomService.GetFreeRoomsForPeriod(reservation.From, reservation.To);
        if (!available.Select(x => x.Id).Contains(reservation.RoomId))
        {
            return false;
        }

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();
        return true;
    }

    private static ReservationViewDto Map(Reservation reservation)
    {
        return new ReservationViewDto
        {
            Id = reservation.Id,
            From = reservation.Start,
            To = reservation.End,
            UserId = reservation.UserId,
            RoomData = new RoomDto
            {
                Id = reservation.RoomId,
                Beds = reservation.Room!.Beds,
                Price = reservation.Room.Price,
                RoomNo = reservation.Room.RoomNo
            }
        };
    }
}