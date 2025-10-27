using System;
using EcommerceSite.Components.DataLayer;
using EcommerceSite.Components.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

class TestActiveOrder
{
    static void Main(string[] args)
    {
        // ASP.NET の ContentRootPath を取るために Host を使う
        var env = Host.CreateDefaultBuilder().Build().Services.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

        if (env == null)
        {
            Console.WriteLine("Environment not found.");
            return;
        }

        var dataLayer = new ActiveOrderDataLayer(env);

        // ① 新しい注文を作成
        Guid seller = Guid.NewGuid();
        Guid buyer = Guid.NewGuid();
        Guid product = Guid.NewGuid();

        ActiveOrder order = dataLayer.CreateActiveOrder(seller, buyer, product);
        Console.WriteLine($"✅ Created Order: {order.Id}");
        Console.WriteLine($"Seller: {order.SellerId}");
        Console.WriteLine($"Buyer:  {order.BuyerId}");
        Console.WriteLine($"Product:{order.ProductId}");

        // ② JSONが更新されたか確認
        Console.WriteLine("\n👉 Check Components/DataBase/ActiveOrder.json");

        // ③ 支払い済みに変更
        dataLayer.MarkPaid(order.Id);
        Console.WriteLine("💰 Order marked as paid.");

        // ④ 発送済みに変更
        dataLayer.MarkShipped(order.Id);
        Console.WriteLine("📦 Order marked as shipped.");

        // 再確認
        ActiveOrder updated = dataLayer.GetOrder(order.Id);
        Console.WriteLine($"\n✅ Paid: {updated.Paid}, Shipped: {updated.Shipped}");
    }
}
