using AirTek.Models;

namespace AirTek.Services;

public interface IOrderService
{
    public List<Order> FetchOrders();
}