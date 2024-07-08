using DataAccess;
using Domain;

namespace Services;

public class RoomService(ReservationContext context) : IRoomService
{
    private readonly ReservationContext _context = context;
    
    public IEnumerable<Room> GetFreeRoomsForPeriod(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}