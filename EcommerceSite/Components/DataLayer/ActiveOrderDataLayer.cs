using System.Text.Json;
using EcommerceSite.Components.Entities;

namespace EcommerceSite.Components;

public class DataLayer
{
    private readonly string _ordersPath;

    public DataLayer(IWebHostEnvironment env)
    {
        _ordersPath = env.ContentRootPath + "/Components/DataBase/ActiveOrders.json";

        if (!File.Exists(_ordersPath))
        {
            string folder = Path.GetDirectoryName(_ordersPath);
            if (folder != null)
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllText(_ordersPath, "[]");
        }
    }

    private List<ActiveOrder> Load()
    {
        string json = File.ReadAllText(_ordersPath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<ActiveOrder>();
        }

        List<ActiveOrder> orders = JsonSerializer.Deserialize<List<ActiveOrder>>(json);

        if (orders == null)
        {
            return new List<ActiveOrder>();
        }

        return orders;
    }

    private void Save(List<ActiveOrder> list)
    {
        string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_ordersPath, json);
    }

    public ActiveOrder CreateActiveOrder(Guid sellerId, Guid buyerId, Guid productId)
    {
        List<ActiveOrder> list = Load();

        ActiveOrder order = new ActiveOrder();
        order.SellerId = sellerId;
        order.BuyerId = buyerId;
        order.ProductId = productId;

        list.Add(order);
        Save(list);
        return order;
    }

    public ActiveOrder GetOrder(Guid orderId)
    {
        List<ActiveOrder> list = Load();

        foreach (ActiveOrder order in list)
        {
            if (order.Id == orderId)
            {
                return order;
            }
        }

        return null;
    }

    public List<ActiveOrder> GetActiveOrdersByBuyer(Guid buyerId)
    {
        List<ActiveOrder> list = Load();
        List<ActiveOrder> result = new List<ActiveOrder>();

        foreach (ActiveOrder order in list)
        {
            if (order.BuyerId == buyerId && order.Shipped == false)
            {
                result.Add(order);
            }
        }

        return result;
    }

    public void MarkPaid(Guid orderId)
    {
        List<ActiveOrder> list = Load();
        bool found = false;

        foreach (ActiveOrder order in list)
        {
            if (order.Id == orderId)
            {
                order.MarkAsPaid();
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new Exception("Order not found.");
        }

        Save(list);
    }

    public void MarkShipped(Guid orderId)
    {
        List<ActiveOrder> list = Load();
        bool found = false;

        foreach (ActiveOrder order in list)
        {
            if (order.Id == orderId)
            {
                order.MarkAsShipped();
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new Exception("Order not found.");
        }

        Save(list);
    }
}
