using DataAccess;
using Domain;
using Services.Dtos;

namespace Services;

public class UserService(ReservationContext context) : IUserService
{
    public async Task AddUser(UserDto dto)
    {
        var toAdd = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Email = dto.Email,
            IdCode = dto.IdCode
        };

        await context.AddAsync(toAdd);
        await context.SaveChangesAsync();
    }


    public async Task<UserDto?> GetUser(int id)
    {
        var result = await context.Users.FindAsync(id);
        return result == null ? null : Map(result);
    }

    private UserDto Map(User result)
    {
        throw new NotImplementedException();
    }
}