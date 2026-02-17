namespace EcommerceSite.Components.Entities;

using System;
using System.Collections.Generic;

public class Products
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;

    public Products() { }

    public Products(string name, decimal price = 0m, string description = "")
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Description = description;
    }
}

public class ProductsCollection
{
    public List<Products> Products { get; set; }

    public ProductsCollection()
    {
        Products = Storage.DataLayer.Load<Products>();
    }

    public void Save()
    {
        Storage.DataLayer.Save(Products);
    }
}
