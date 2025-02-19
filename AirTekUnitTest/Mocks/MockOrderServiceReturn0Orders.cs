using AirTek.Models;
using AirTek.Services;

namespace AirTekUnitTest.Mocks;
public class MockOrderServiceReturn0Orders : IOrderService
{
    public List<Order> FetchOrders()
    {
        // Return empty list
        return new List<Order>();
    }
}