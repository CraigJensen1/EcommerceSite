using System;
using System.Text.Json.Serialization;

namespace EcommerceSite.Components.Entities;

public class ActiveOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ProductId { get; set; }

    public bool Paid { get; private set; } = false;
    public bool Shipped { get; private set; } = false;

    public void MarkAsPaid()
    {
        Paid = true;
    }

    public void MarkAsShipped()
    {
        if (!Paid)
        {
            throw new InvalidOperationException("Order must be paid before it can be shipped.");
        }
        Shipped = true;
    }
}

public class ActiveOrderCollection
{
    public List<ActiveOrder> ActiveOrders { get; set; }
    public ActiveOrderCollection()
    {
        ActiveOrders = Storage.DataLayer.Load<ActiveOrder>();
    }
    public void Save()
    {
        Storage.DataLayer.Save(ActiveOrders);
    }
        
    public ActiveOrder GetOrder(Guid orderId)
    {
        

        foreach (ActiveOrder order in ActiveOrders)
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
    
        List<ActiveOrder> result = new List<ActiveOrder>();

        foreach (ActiveOrder order in ActiveOrders)
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

        bool found = false;

        foreach (ActiveOrder order in ActiveOrders)
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

        Save();
    }

    public void MarkShipped(Guid orderId)
    {
    
        bool found = false;

        foreach (ActiveOrder order in ActiveOrders)
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

        Save();
    }
    }