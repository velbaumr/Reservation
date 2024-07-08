using DataAccess;
using Domain;

namespace Services;

public class UserService(ReservationContext context) : IUserService
{
    public Task AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(int id)
    {
        throw new NotImplementedException();
    }
}