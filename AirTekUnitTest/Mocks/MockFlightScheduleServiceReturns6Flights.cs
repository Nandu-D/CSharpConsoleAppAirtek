using AirTek.Models;
using AirTek.Services;

namespace AirTekUnitTest.Mocks;

public class MockFlightScheduleServiceReturns6Flights : IFlightScheduleService
{
    public List<FlightSchedule> FetchFlightSchedules()
    {
        // day1Flight1 has a capacity of 1
        var day1Flight1 = new FlightSchedule(1, 1, "Montreal", "YUL", "Toronto", "YYZ", 1);
        var day1Flight2 = new FlightSchedule(1, 2, "Montreal", "YUL", "Calgary", "YYC");
        var day1Flight3 = new FlightSchedule(1, 3, "Montreal", "YUL", "Vancouver", "YVR");
        var day2Flight4 = new FlightSchedule(2, 4, "Montreal", "YUL", "Toronto", "YYZ");
        var day2Flight5 = new FlightSchedule(2, 5, "Montreal", "YUL", "Calgary", "YYC");
        var day2Flight6 = new FlightSchedule(2, 6, "Montreal", "YUL", "Vancouver", "YVR");

        // Jumbled order of items in list
        var flightScheduleList = new List<FlightSchedule>() {day1Flight1, day2Flight4, day1Flight3,
            day1Flight2, day2Flight6, day2Flight5};

        return flightScheduleList;
    }
}