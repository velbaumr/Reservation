using Domain;

namespace Services;

public interface IUserService
{
    Task AddUser(User user);

    Task<IEnumerable<User>> GetUsers();

    Task<User> GetUser(int id);
}