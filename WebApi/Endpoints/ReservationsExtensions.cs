using System.ComponentModel.DataAnnotations;
using FluentValidation;
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
        app.MapPost("api/reservation",
                async (IReservationService service, IValidator<ReservationDto> validator, [FromBody] ReservationDto dto) =>
                {
                    var validationResult = await validator.ValidateAsync(dto);
                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                    var result = await service.AddReservation(dto);
                    return result ? Results.Ok(result) : Results.BadRequest("Room already reserved");
                    
                })
                .WithOpenApi()
            .WithName("CreateReservation");
        app.MapPatch("api/reservation", async (IReservationService service, IValidator<ReservationDto> validator, [FromBody] ReservationDto dto) =>
            {
                var result = await service.UpdateReservation(dto);
                return result ? Results.Ok(result) : Results.NotFound();
            })
            .WithOpenApi()
            .WithName("EditReservation");
        app.MapPatch("api/reservation/cancel/user/{userId:int}/reservation/{reservationId:int}",
                async (IReservationService service, int reservationId, int userId) =>
                {
                    await service.CancelReservation(reservationId, userId);
                    return Results.Ok();
                })
            .WithOpenApi()
            .WithName("CancelReservation");
    }
}