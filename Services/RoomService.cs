using DataAccess;
using Domain;

namespace Services;

public class RoomService(ReservationContext context) : IRoomService
{
  
    public IEnumerable<Room> GetFreeRoomsForPeriod(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}