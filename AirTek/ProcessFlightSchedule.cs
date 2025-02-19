using AirTek.Models;
using AirTek.Services;

namespace AirTek;

public class ProcessFlightSchedule
{
    List<FlightSchedule>? FlightSchedulesList;
    readonly IFlightScheduleService FlightScheduleService;
    public ProcessFlightSchedule(IFlightScheduleService flightScheduleService)
    {
        FlightScheduleService = flightScheduleService;
    }
    public void RetrieveAndSortFlightSchedule()
    {
        FlightSchedulesList = FlightScheduleService.FetchFlightSchedules();
        FlightSchedulesList.Sort((x, y) =>
        {
            if (x.Day < y.Day) return -1;
            if (x.Day == y.Day && x.FlightNumber < y.FlightNumber) return -1;
            if (x.Day == y.Day && x.FlightNumber == y.FlightNumber) return 0;
            return 1;
        });
    }
    public void PrintFlightSchedule()
    {
        List<string> formattedOutputFlightScheduleList = ReturnFormattedFlightScheduleStringList();

        Console.WriteLine("\nFlight Schedule");
        foreach (string flightScheduleOutputLine in formattedOutputFlightScheduleList)
        {
            Console.WriteLine(flightScheduleOutputLine);
        }
    }
    public List<string> ReturnFormattedFlightScheduleStringList()
    {
        if (FlightSchedulesList == null)
        {
            RetrieveAndSortFlightSchedule();
        }

        List<string> formattedOutputFlightScheduleList = new List<string>();
        foreach (FlightSchedule flightSchedule in FlightSchedulesList)
        {
            string formattedOutputLine = $"Flight: {flightSchedule.FlightNumber}, departure: {flightSchedule.DepartureAirportCode}, arrival: {flightSchedule.DestinationAirportCode}, day: {flightSchedule.Day}";
            formattedOutputFlightScheduleList.Add(formattedOutputLine);
        }
        return formattedOutputFlightScheduleList;
    }
}