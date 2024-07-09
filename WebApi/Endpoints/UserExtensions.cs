using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace WebApi.Endpoints;

public static class UserExtensions
{
    public static void MapUsers(this WebApplication app)
    {
        app.MapPost("api/users", async (IUserService service, IValidator<UserDto> validator, [FromBody] UserDto dto) =>
            {
                var validationResult = await validator.ValidateAsync(dto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                await service.AddUser(dto);
                return Results.Ok();
            })
            .WithOpenApi()
            .WithName("AddUser");

        app.MapGet("api/users/{userId:int}", async (IUserService service, int userId) =>
            {
                var result = await service.GetUser(userId);
                return result == null ? Results.NotFound("Not found") : Results.Ok(result);
            })
            .WithOpenApi()
            .WithName("GetUser");
        app.MapGet("api/users/email/{email}", async (IUserService service, string email) =>
            {
                var result = await service.GetByEmail(email);
                return result == null ? Results.NotFound("Not found") : Results.Ok(result);
            })
            .WithOpenApi()
            .WithName("GetUserByEmail");
    }
}