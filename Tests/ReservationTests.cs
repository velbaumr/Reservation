using Domain;

namespace Tests;

public class ReservationTests
{
    [Fact]
    public void ReservationEnded()
    {
        var reservation = new Reservation
        {
            End = new DateTime(2024, 7, 12),
            Today = new DateTime(2024, 7, 13)
        };

        Assert.True(reservation.Ended);
    }

    [Fact]
    public void ReservationNotEnded()
    {
        var reservation = new Reservation
        {
            End = new DateTime(2024, 7, 12),
            Today = new DateTime(2024, 7, 12)
        };

        Assert.True(reservation.Ended);
    }

    [Fact]
    public void ReservationInUseStart()
    {
        var reservation = new Reservation
        {
            Start = new DateTime(2024, 7, 12),
            End = new DateTime(2024, 7, 13),
            Today = new DateTime(2024, 7, 12)
        };

        Assert.True(reservation.InUse);
    }


    [Fact]
    public void ReservationNotInUseStart()
    {
        var reservation = new Reservation
        {
            Start = new DateTime(2024, 7, 12),
            End = new DateTime(2024, 7, 13),
            Today = new DateTime(2024, 7, 11)
        };

        Assert.True(!reservation.InUse);
    }
    
    [Fact]
    public void ReservationInUseEnd()
    {
        var reservation = new Reservation
        {
            Start = new DateTime(2024, 7, 12),
            End = new DateTime(2024, 7, 13),
            Today = new DateTime(2024, 7, 13)
        };

        Assert.True(reservation.InUse);
    }


    [Fact]
    public void ReservationNotInUseEnd()
    {
        var reservation = new Reservation
        {
            Start = new DateTime(2024, 7, 12),
            End = new DateTime(2024, 7, 13),
            Today = new DateTime(2024, 7, 14)
        };

        Assert.True(!reservation.InUse);
    }
}