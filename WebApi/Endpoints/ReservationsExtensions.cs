using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace WebApi.Endpoints;

public static class ReservationsExtensions
{
    public static void MapReservations(this WebApplication app)
    {
        app.MapGet("api/reservation/user/{userId:int}",
            async (IReservationService service, int userId) => await service.GetReservationsForUser(userId))
            .WithOpenApi()
            .WithName("GetUsersReservations");
        app.MapGet("api/reservation/",
            async (IReservationService service) => await service.GetValidReservations())
            .WithOpenApi()
            .WithName("GetAllValidReservations");
        app.MapPost("api/reservation", async (IReservationService service, [FromBody] ReservationDto dto) => await service.AddReservation(dto))
            .WithOpenApi()
            .WithName("CreateReservation");
        app.MapPatch("api/reservation", async (IReservationService service, [FromBody] ReservationDto dto) =>
            {
                var result = await service.UpdateReservation(dto);
                return result == null ? Results.NotFound("Not found") : Results.Ok();
            }) 
            .WithOpenApi()
            .WithName("EditReservation");
        app.MapPatch("api/reservation/cancel/{reservationId:int}",
            async (IReservationService service, int reservationId) =>
            {
                await service.CancelReservation(reservationId);
                return Results.Ok();
            })
            .WithOpenApi()
            .WithName("CancelReservation");
    }
}

