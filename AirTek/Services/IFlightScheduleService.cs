using AirTek.Models;

namespace AirTek.Services;


public interface IFlightScheduleService
{
    public List<FlightSchedule> FetchFlightSchedules();
}
