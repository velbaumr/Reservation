using Services.Dtos;

namespace Services;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetFreeRoomsForPeriod(DateTime start, DateTime end);
}