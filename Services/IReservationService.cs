using Domain;
using Services.Dtos;


namespace Services;

public interface IReservationService
{
    Task<IEnumerable<ReservationViewDto?>> GetValidReservations();
    Task<IEnumerable<ReservationViewDto?>> GetReservationsForUser(int userId);
   
    Task CancelReservation(int reservationId);

    Task<ReservationViewDto?> AddReservation(ReservationDto reservation);
    
    Task<ReservationViewDto?> UpdateReservation(ReservationDto reservation);
}