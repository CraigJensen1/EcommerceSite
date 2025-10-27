using Microsoft.AspNetCore.Components;
using EcommerceSite.Components.Entities;

namespace EcommerceSite.Pages;

public partial class AllProducts : ComponentBase
{
    private List<EcommerceSite.Components.Entities.Product> products;

    protected override void OnInitialized()
    {
        var collection = new ProductCollection();
        products = collection.Product.ToList();
    }
}
