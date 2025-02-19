using AirTek.Models;
using AirTek.Services;

namespace AirTekUnitTest.Mocks;

public class MockFlightScheduleServiceReturns0Flights : IFlightScheduleService
{
    public List<FlightSchedule> FetchFlightSchedules()
    {
        // Return empty list
        return new List<FlightSchedule>();
    }
}