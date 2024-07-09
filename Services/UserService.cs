using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services;

public class UserService(ReservationContext context) : IUserService
{
    public async Task AddUser(UserDto user)
    {
        var toAdd = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            IdCode = user.IdCode
        };

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();
    }


    public async Task<UserDto?> GetUser(int id)
    {
        var result = await context.Users.FindAsync(id);
        return result == null ? null : Map(result);
    }

    public async Task<UserDto?> GetByEmail(string email)
    {
        var result = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        return result == null ? null : Map(result);
    }

    private static UserDto Map(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            IdCode = user.IdCode,
            IsAdmin = user.IsAdmin
        };
    }
}