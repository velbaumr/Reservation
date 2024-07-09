namespace WebApi.Endpoints;

public static class RoomExtensions
{
    public static void MapRooms(this WebApplication app)
    {
        app.MapGet("", () => { });
    }
}