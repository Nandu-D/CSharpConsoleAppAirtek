using AirTek.Models;
using AirTek.Services;

namespace AirTekUnitTest.Mocks;
public class MockOrderServiceReturn5Orders : IOrderService
{
    public List<Order> FetchOrders()
    {
        var order1 = new Order("order-001", "YYZ");
        var order2 = new Order("order-002", "YYC");
        var order3 = new Order("order-003", "YVR");
        var order4 = new Order("order-004", "YEE");
        var order5 = new Order("order-005", "YYZ");

        // Jumbled order of items in list
        List<Order> orders = new List<Order>() { order5, order1, order3, order2, order4 };
        return orders;
    }
}