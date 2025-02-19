using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using AirTek.Models;

namespace AirTek.Services;

public class OrderService : IOrderService
{
    public List<Order> FetchOrders()
    {
        List<Order> orders = new List<Order>();

        JsonNode? jsonNode = null;
        try
        {
            var jsonAsString = File.ReadAllText("coding-assigment-orders.json");
            jsonNode = JsonSerializer.Deserialize<JsonNode>(jsonAsString);
        }
        catch (Exception)
        {
            Console.WriteLine("An error occurred when processing json file");
        }

        if (jsonNode != null)
        {
            foreach (KeyValuePair<string, JsonNode?> kv in jsonNode.AsObject())
            {
                string orderName = kv.Key;
                JsonNode? destinationObj;
                string destinationAirportCode;
                if (kv.Value != null)
                {
                    destinationObj = kv.Value.AsObject();
                    if (destinationObj != null)
                    {
                        var destinationObjDestination = destinationObj["destination"];
                        if (destinationObjDestination != null)
                        {
                            destinationAirportCode = destinationObjDestination.ToString();
                            orders.Add(new Order(orderName, destinationAirportCode));
                        }
                    }
                }
            }
        }
        return orders;
    }
}