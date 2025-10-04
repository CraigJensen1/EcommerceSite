using System;
using System.Text.Json.Serialization;

namespace EcommerceSite.Components.Entities;

public class ActiveOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ProductId { get; set; }

    public bool Paid { get; set; } = false;
    public bool Shipped { get; set; } = false;
    
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