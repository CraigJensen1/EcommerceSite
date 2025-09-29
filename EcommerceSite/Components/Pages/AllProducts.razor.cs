using Microsoft.AspNetCore.Components;
namespace EcommerceSite.Pages;

public partial class AllProducts : ComponentBase
{
    private int currentCount { get; set; } = 1;

    private void IncrementCount()
    {
        currentCount++;
    }
}