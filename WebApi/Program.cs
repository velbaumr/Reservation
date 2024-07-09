using DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Dtos;
using WebApi.Endpoints;
using WebApi.Handlers;
using WebApi.Validators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IValidator<ReservationDto>, ReservationValidator>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapReservations();
app.MapRooms();
app.MapUsers();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.Run();