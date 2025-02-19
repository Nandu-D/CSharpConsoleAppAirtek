namespace AirTek.Models;

public class FlightSchedule
{
    public int Day;
    public int FlightNumber;
    public string DepartureAirportName;
    public string DepartureAirportCode;
    public string DestinationAirportName;
    public string DestinationAirportCode;
    public int MaxCargoCapacity;
    public List<Order> ListOfCargoLoaded;

    public FlightSchedule(int day, int flightNumber, string departureAirportName, string departureAirportCode,
        string destinationAirportName, string destinationAirportCode, int maxCargoCapacity = 20)
    {
        Day = day;
        FlightNumber = flightNumber;
        DepartureAirportName = departureAirportName;
        DepartureAirportCode = departureAirportCode;
        DestinationAirportName = destinationAirportName;
        DestinationAirportCode = destinationAirportCode;
        MaxCargoCapacity = maxCargoCapacity;
        ListOfCargoLoaded = new List<Order>();
    }
}