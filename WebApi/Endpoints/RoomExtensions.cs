using Services;

namespace WebApi.Endpoints;

public static class RoomExtensions
{
    public static void MapRooms(this WebApplication app)
    {
        app.MapGet("api/rooms/start/{start:datetime}/end/{end:datetime}",
            (IRoomService service, DateTime start, DateTime end) => service.GetFreeRoomsForPeriod(start, end));
    }
}