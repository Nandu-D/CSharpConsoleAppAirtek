namespace AirTek.Models;

public class NotNeededFlightAndOrderAssignment
{
    public int MaxCargoCapacity;
    public FlightSchedule FlightSchedule;
    public List<Order> ListOfCargoLoaded;

    public NotNeededFlightAndOrderAssignment(FlightSchedule flightSchedule, int maxCargoCapacity = 20)
    {
        MaxCargoCapacity = maxCargoCapacity;
        FlightSchedule = flightSchedule;
        ListOfCargoLoaded = new List<Order>();
    }
}