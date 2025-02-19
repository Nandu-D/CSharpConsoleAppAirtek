namespace AirTek.Models;

public class Order
{
    public string Name;
    public string DestinationAirportCode;
    public FlightSchedule? ScheduledToBeOnFlight;

    public Order(string name, string destinationAirportCode)
    {
        Name = name;
        DestinationAirportCode = destinationAirportCode;
    }
}