using AirTek.Services;

namespace AirTek;
class Program
{
    private static FlightScheduleService? flightScheduleService;
    private static OrderService? orderService;
    private static ProcessFlightSchedule? processFlightSchedule;
    private static ProcessOrders? processOrders;
    static void Main(string[] args)
    {
        Console.WriteLine("Hi, please enter an option from below or press any other key to exit");

        bool toContinue = true;
        while (toContinue)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("Please press \"1\" to view list of flight schedules");
            Console.WriteLine("Please press \"2\" to view a list order assignments");
            Console.WriteLine("To exit the program, please press any other key");

            var chosenOption = Console.ReadKey().KeyChar;
            switch (chosenOption)
            {
                case '1':
                    if (flightScheduleService == null)
                        flightScheduleService = new FlightScheduleService();
                    if (processFlightSchedule == null)
                        processFlightSchedule = new ProcessFlightSchedule(flightScheduleService);
                    processFlightSchedule.PrintFlightSchedule();
                    break;
                case '2':
                    if (orderService == null)
                        orderService = new OrderService();
                    if (flightScheduleService == null)
                        flightScheduleService = new FlightScheduleService();
                    if (processOrders == null)
                        processOrders = new ProcessOrders(orderService, flightScheduleService);
                    processOrders.PrintOrderAssignment();
                    break;
                default:
                    toContinue = false;
                    break;
            }
        }
    }
}
