using AirTek;
using AirTek.Services;
using AirTekUnitTest.Mocks;

namespace AirTekUnitTest;

[TestClass]
public class ScheduleOrderToFlightAndReturnOutputStringsTest
{
    private List<string>? orderOutputStringList;

    [TestMethod]
    public void TestScheduleOrderToFlightAndReturnOutputStrings_HasTheExpectedOutput()
    {
        IOrderService orderService = new MockOrderServiceReturn5Orders();
        IFlightScheduleService flightScheduleService = new MockFlightScheduleServiceReturns6Flights();

        var processOrders = new ProcessOrders(orderService, flightScheduleService);
        orderOutputStringList = processOrders.ScheduleOrderToFlightAndReturnOutputStringList();

        if (orderOutputStringList == null)
        {
            Assert.Fail("Order assignment output string is null which shouldn't be the case");
        }
        else
        {
            Assert.AreEqual(5, orderOutputStringList.Count);
            Assert.AreEqual("order: order-001, flightNumber: 1, departure: YUL, arrival: YYZ, day: 1", orderOutputStringList[0]);
            Assert.AreEqual("order: order-002, flightNumber: 2, departure: YUL, arrival: YYC, day: 1", orderOutputStringList[1]);
            Assert.AreEqual("order: order-003, flightNumber: 3, departure: YUL, arrival: YVR, day: 1", orderOutputStringList[2]);
            Assert.AreEqual("order: order-004, flightNumber: not scheduled", orderOutputStringList[3]);
            Assert.AreEqual("order: order-005, flightNumber: 4, departure: YUL, arrival: YYZ, day: 2", orderOutputStringList[4]);
        }
    }

    [TestMethod]
    public void TestScheduleOrderToFlightAndReturnOutputStrings_CauseNoExceptionForEmptyOrdersAndFlightsList()
    {
        IOrderService orderService = new MockOrderServiceReturn0Orders();
        IFlightScheduleService flightScheduleService = new MockFlightScheduleServiceReturns0Flights();

        var processOrders = new ProcessOrders(orderService, flightScheduleService);
        try
        {
            orderOutputStringList = processOrders.ScheduleOrderToFlightAndReturnOutputStringList();
            if (orderOutputStringList == null)
            {
                Assert.Fail("Order assignment output string is null which shouldn't be the case");
            }
            else
            {
                Assert.AreEqual(0, orderOutputStringList.Count);
            }
        }
        catch (Exception e)
        {
            Assert.Fail("The call to ScheduleOrderToFlightAndReturnOutputStringList resulted in an exception: " + e.Message);
        }
    }
}
