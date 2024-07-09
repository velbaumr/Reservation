using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services;

public class ReservationService(
    ReservationContext context, IUserService service) : IReservationService
{
    public async Task<IEnumerable<ReservationViewDto?>> GetValidReservations()
    {
        var start = DateTime.Today;

        var reservations = await context.Reservations.Include(x => x.Room)
            .Where(x =>
                x.Start >= start &&
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
        var user = await service.GetUser(userId);
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


    public async Task<ReservationViewDto?> UpdateReservation(ReservationDto reservation)
    {
        var toUpdate = await context.Reservations.FindAsync(reservation.Id);

        if (toUpdate == null)
            return null;
        toUpdate.Start = reservation.From;
        toUpdate.End = reservation.To;

        await context.SaveChangesAsync();

        return Map(toUpdate);
    }

    public async Task<ReservationViewDto?> AddReservation(ReservationDto reservation)
    {
        var toAdd = new Reservation
        {
            Start = reservation.From,
            End = reservation.To,
            RoomId = reservation.RoomId,
            UserId = reservation.UserId
        };

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();

        return Map(toAdd);
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