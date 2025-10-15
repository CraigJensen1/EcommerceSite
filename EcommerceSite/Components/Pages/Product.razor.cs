using Microsoft.AspNetCore.Components;
using EcommerceSite.Components.Entities;
using EcommerceSite.Components.Classes;

namespace EcommerceSite.Pages;

public partial class Product : ComponentBase
{
    // For simplicity we identify product by query parameter id (Guid) and current user id by query param userId (Guid)
    [Parameter]
    [SupplyParameterFromQuery]
    public Guid? id { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public Guid? userId { get; set; }

    protected Products? product;
    protected List<Reviews> ProductReviews { get; set; } = new List<Reviews>();
    protected bool CanReview { get; set; } = false;

    protected int NewRating { get; set; } = 5;
    protected string NewComment { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        // load product
        var products = new ProductsCollection();
        if (id.HasValue)
        {
            product = products.Products.FirstOrDefault(p => p.Id == id.Value);
        }

        // load reviews for this product
        var reviewsCollection = new ReviewsCollection();
        if (product != null)
        {
            ProductReviews = reviewsCollection.Reviews.Where(r => r.ProductId == product.Id).OrderByDescending(r => r.Created).ToList();
        }

        // determine if user purchased this product
        CanReview = false;
        if (userId.HasValue && product != null)
        {
            var sales = new SalesHistoryCollection();
            CanReview = sales.SalesHistory.Any(s => s.CustomerID == userId.Value && s.ProductID == product.Id);
        }
    }

    protected void SubmitReview()
    {
        if (product == null || !userId.HasValue) return;
        var reviewsCollection = new ReviewsCollection();
        var newReview = new Reviews(product.Id, userId.Value, Math.Clamp(NewRating, 1, 5), NewComment ?? string.Empty);
        reviewsCollection.Reviews.Add(newReview);
        reviewsCollection.Save();

        // refresh in-memory list
        ProductReviews.Insert(0, newReview);

        // reset form
        NewRating = 5;
        NewComment = string.Empty;
    }

    // helper for star rendering
    protected string Stars(int rating)
    {
        if (rating < 0) rating = 0;
        if (rating > 5) rating = 5;
        return new string('★', rating) + new string('☆', 5 - rating);
    }
}