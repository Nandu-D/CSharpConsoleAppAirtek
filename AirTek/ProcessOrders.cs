using AirTek.Models;
using AirTek.Services;

namespace AirTek;

public class ProcessOrders
{
    public List<Order>? OrdersList;
    public List<FlightSchedule>? FlightSchedulesList;
    readonly IOrderService OrderService;
    readonly IFlightScheduleService FlightScheduleService;
    public ProcessOrders(IOrderService orderService, IFlightScheduleService flightScheduleService)
    {
        OrderService = orderService;
        FlightScheduleService = flightScheduleService;
    }
    public void PrintOrderAssignment()
    {
        List<string>? orderDetailsOutputList = ScheduleOrderToFlightAndReturnOutputStringList();
        Console.WriteLine("\nOrder And Associated Flight Schedule");
        if (orderDetailsOutputList != null)
        {
            foreach (string orderDetailsOutputLine in orderDetailsOutputList)
            {
                Console.WriteLine(orderDetailsOutputLine);
            }
        }
    }
    public List<string>? ScheduleOrderToFlightAndReturnOutputStringList()
    {
        AssignOrdersToFlights();
        if (OrdersList != null)
        {
            OrdersList.Sort((x, y) => String.Compare(x.Name, y.Name));
            List<string> outputList = new List<string>();
            foreach (Order order in OrdersList)
            {
                FlightSchedule flightSchedule = order.ScheduledToBeOnFlight;
                if (flightSchedule == null)
                {
                    // No flight is scheduled for the order
                    outputList.Add($"order: {order.Name}, flightNumber: not scheduled");
                }
                else
                {
                    outputList.Add($"order: {order.Name}, flightNumber: {flightSchedule.FlightNumber}, departure: {flightSchedule.DepartureAirportCode}, arrival: {flightSchedule.DestinationAirportCode}, day: {flightSchedule.Day}");
                }
            }
            return outputList;
        }
        return null;
    }

    /// <summary>
    /// This method fetches order and flight details. It builds a dictionary of flights with destination 
    /// as the key and a priority queue holding flights to that destination as the value. It also builds a 
    /// priority queue holding orders with order name like "order-001" as key for calculating priority. 
    /// The function iterates through the orders in priority queue by taking each of the order with the 
    /// highest priority in succession. The destination of the order is used to fetch flights to that 
    /// destination by using destination airport code to search for flights to that destination. In the 
    /// priority queue of flights to the destination the flight with the highest priority is taken and 
    /// order is assigned to it. If the flight has reached capacity then the flight is removed from this 
    /// queue.
    /// </summary>
    public void AssignOrdersToFlights()
    {
        if (OrdersList == null)
        {
            OrdersList = OrderService.FetchOrders();
        }
        if (FlightSchedulesList == null)
        {
            FlightSchedulesList = FlightScheduleService.FetchFlightSchedules();
        }

        Dictionary<string, PriorityQueue<FlightSchedule, int>>? flightsScheduleDestinationDictionary =
            new Dictionary<string, PriorityQueue<FlightSchedule, int>>();

        // Iterate through flights and add them to a dictionary with destination airport code as key and a 
        // min priority queue as value. Min priority queue hold flights going to the same destination with
        // earlier flights having more priority. It uses day number to determine earlier flights. 
        foreach (FlightSchedule flightSchedule in FlightSchedulesList)
        {
            string destinationAirportCode = flightSchedule.DestinationAirportCode;
            if (flightsScheduleDestinationDictionary.ContainsKey(destinationAirportCode))
            {
                flightsScheduleDestinationDictionary[destinationAirportCode].Enqueue(flightSchedule, flightSchedule.Day);
            }
            else
            {
                //var priorityQueue = new PriorityQueue<FlightSchedule, int>(Comparer<int>.Create((x,y)=>y.CompareTo(x)));
                PriorityQueue<FlightSchedule, int> priorityQueue = new PriorityQueue<FlightSchedule, int>();
                priorityQueue.Enqueue(flightSchedule, flightSchedule.Day);
                flightsScheduleDestinationDictionary.Add(destinationAirportCode, priorityQueue);
            }
        }

        // Add orders to a min priority queue with order name as key to determine priority
        PriorityQueue<Order, string>? orderPriorityQueue = new PriorityQueue<Order, string>(Comparer<string>.Create((x, y) => String.CompareOrdinal(x, y)));
        foreach (Order order in OrdersList)
        {
            orderPriorityQueue.Enqueue(order, order.Name);
        }

        // Dequeue each order and assign it to the highest priority flight with capacity if possible
        while (orderPriorityQueue.Count > 0)
        {
            Order highestPriorityOrder = orderPriorityQueue.Dequeue();
            string orderDestinationAirportCode = highestPriorityOrder.DestinationAirportCode;

            // See if there are flights available for the destination by checking flightsScheduleDestinationDictionary
            if (flightsScheduleDestinationDictionary.ContainsKey(orderDestinationAirportCode))
            {
                PriorityQueue<FlightSchedule, int> flightsPriorityQueue = flightsScheduleDestinationDictionary[orderDestinationAirportCode];
                if (flightsPriorityQueue.Count > 0)
                {
                    FlightSchedule highestPriorityFlight = flightsPriorityQueue.Peek();
                    int currentCargoCount = highestPriorityFlight.ListOfCargoLoaded.Count;
                    int maxCargoCapacity = highestPriorityFlight.MaxCargoCapacity;
                    if (currentCargoCount < maxCargoCapacity)
                    {
                        // Add order to list of cargo in flight
                        highestPriorityFlight.ListOfCargoLoaded.Add(highestPriorityOrder);
                        // Add flight scheduled to be on to order 
                        highestPriorityOrder.ScheduledToBeOnFlight = highestPriorityFlight;
                        if (highestPriorityFlight.ListOfCargoLoaded.Count == maxCargoCapacity)
                        {
                            // If flight is at cargo capacity remove it from the list of flights to assign cargo to
                            flightsPriorityQueue.Dequeue();
                        }
                    }
                    else
                    {
                        flightsScheduleDestinationDictionary.Remove(orderDestinationAirportCode);
                    }
                }
                else
                {
                    // No flights available to hold cargo for the destination. So remove it from dictionary
                    flightsScheduleDestinationDictionary.Remove(orderDestinationAirportCode);
                }
            }
        }
        // Setting the flight schedule dictionary and order priority queue to null as they are no longer needed
        flightsScheduleDestinationDictionary = null;
        orderPriorityQueue = null;
    }
}