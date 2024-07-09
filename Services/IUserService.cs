using Services.Dtos;

namespace Services;

public interface IUserService
{
    Task AddUser(UserDto user);
    Task<UserDto?> GetUser(int id);
    Task<UserDto?> GetByEmail(string email);
}