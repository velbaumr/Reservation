using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services;

public class RoomService(IReservationService service, ReservationContext context) : IRoomService
{
    public async Task<IEnumerable<RoomDto>> GetFreeRoomsForPeriod(DateTime start, DateTime end)
    {
        var reserved = await service.GetValidReservations();
        var reservedForPeriod = reserved.Where(x => x!.From < end && x.To > start)
            .Select(x => x!.Id);
        
        var all = await context.Rooms.ToListAsync();
        
        var notReserved = all.Where(x => !reservedForPeriod.Contains(x.Id)).ToList();
 
        return notReserved.Select(Map);
    }

    private static RoomDto Map(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Beds = room.Beds,
            Price = room.Price,
            RoomNo = room.RoomNo
        };
    }
}