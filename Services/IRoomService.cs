using Domain;

namespace Services;

public interface IRoomService
{
    IEnumerable<Room> GetFreeRoomsForPeriod(DateTime start, DateTime end);
}