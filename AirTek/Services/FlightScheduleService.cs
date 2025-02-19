using AirTek.Models;

namespace AirTek.Services;

public class FlightScheduleService : IFlightScheduleService
{
    public List<FlightSchedule> FetchFlightSchedules()
    {
        var day1Flight1 = new FlightSchedule(1, 1, "Montreal", "YUL", "Toronto", "YYZ");
        var day1Flight2 = new FlightSchedule(1, 2, "Montreal", "YUL", "Calgary", "YYC");
        var day1Flight3 = new FlightSchedule(1, 3, "Montreal", "YUL", "Vancouver", "YVR");
        var day2Flight4 = new FlightSchedule(2, 4, "Montreal", "YUL", "Toronto", "YYZ");
        var day2Flight5 = new FlightSchedule(2, 5, "Montreal", "YUL", "Calgary", "YYC");
        var day2Flight6 = new FlightSchedule(2, 6, "Montreal", "YUL", "Vancouver", "YVR");

        var flightScheduleList = new List<FlightSchedule>() {day1Flight1, day1Flight2, day1Flight3,
            day2Flight4, day2Flight5, day2Flight6};

        return flightScheduleList;
    }
}
