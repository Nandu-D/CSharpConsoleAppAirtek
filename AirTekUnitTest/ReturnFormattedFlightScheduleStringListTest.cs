using AirTek;
using AirTek.Services;
using AirTekUnitTest.Mocks;

namespace AirTekUnitTest;

[TestClass]
public class ReturnFormattedFlightScheduleStringListTest
{
    private List<string>? flightScheduleResultStringList;

    [TestMethod]
    public void TestFlightScheduleOutputStringList_HasTheExpectedOutput()
    {
        IFlightScheduleService flightScheduleService = new MockFlightScheduleServiceReturns6Flights();
        var processFlightSchedule = new ProcessFlightSchedule(flightScheduleService);
        flightScheduleResultStringList = processFlightSchedule.ReturnFormattedFlightScheduleStringList();

        Assert.AreEqual(6, flightScheduleResultStringList.Count);
        Assert.AreEqual("Flight: 1, departure: YUL, arrival: YYZ, day: 1", flightScheduleResultStringList[0]);
        Assert.AreEqual("Flight: 2, departure: YUL, arrival: YYC, day: 1", flightScheduleResultStringList[1]);
        Assert.AreEqual("Flight: 3, departure: YUL, arrival: YVR, day: 1", flightScheduleResultStringList[2]);
        Assert.AreEqual("Flight: 4, departure: YUL, arrival: YYZ, day: 2", flightScheduleResultStringList[3]);
        Assert.AreEqual("Flight: 5, departure: YUL, arrival: YYC, day: 2", flightScheduleResultStringList[4]);
        Assert.AreEqual("Flight: 6, departure: YUL, arrival: YVR, day: 2", flightScheduleResultStringList[5]);
    }

    [TestMethod]
    public void TestFlightScheduleOutputStringList_CauseNoExceptionForEmptyFlightsList()
    {
        IFlightScheduleService flightScheduleService = new MockFlightScheduleServiceReturns0Flights();
        var processFlightSchedule = new ProcessFlightSchedule(flightScheduleService);

        try
        {
            flightScheduleResultStringList = processFlightSchedule.ReturnFormattedFlightScheduleStringList();
            if (flightScheduleResultStringList == null)
            {
                Assert.Fail("Flight schedule output string is null which shouldn't be the case");
            }
            else
            {
                Assert.AreEqual(0, flightScheduleResultStringList.Count);
            }
        }
        catch (Exception e)
        {
            Assert.Fail("The call to ReturnFormattedFlightScheduleStringList resulted in an exception: " + e.Message);
        }
    }
}