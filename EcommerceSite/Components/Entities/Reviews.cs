namespace EcommerceSite.Components.Entities;

using System;
using System.Collections.Generic;

public class Reviews
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime Created { get; set; }

    public Reviews() { }

    public Reviews(Guid productId, Guid customerId, int rating, string comment = "")
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        CustomerId = customerId;
        Rating = rating;
        Comment = comment;
        Created = DateTime.UtcNow;
    }
}

public class ReviewsCollection
{
    public List<Reviews> Reviews { get; set; }

    public ReviewsCollection()
    {
        Reviews = Storage.DataLayer.Load<Reviews>();
    }

    public void Save()
    {
        Storage.DataLayer.Save(Reviews);
    }
}
