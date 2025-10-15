using EcommerceSite.Components.Storage;

namespace EcommerceSite.Components.Entities
{
    public class ProductFiltering
    {

        public List<Product> FilterByCategory(string category)
        {
            ProductCollection collection = new ProductCollection();
            List<Product> products = new List<Product>();
            

            for (int i = 0; i < collection.Product.Count; i++)
            {
                if(category == collection.Product[i].Category)
                {
                    products.Add(collection.Product[i]);
                }
            }

            return products;
        }

        public void SortByLowestPrice()
        {
            ProductCollection collection = new ProductCollection();


            int j = 1;
            Product productSwap =  new Product();

            for (int i = 1; i < collection.Product.Count; i++)
            {
                j = i;
                while (j > 0 && collection.Product[j - 1].Price > collection.Product[j].Price)
                {

                    productSwap = collection.Product[j];
                    collection.Product[j] = collection.Product[j - 1];
                    collection.Product[j - 1] = productSwap;
                    j--;
                }
            }
        }

        public void SortByHighestPrice()
        {
            ProductCollection collection = new ProductCollection();


            int j = 1;
            Product productSwap = new Product();

            for (int i = 1; i < collection.Product.Count; i++)
            {
                j = i;
                while (j > 0 && collection.Product[j - 1].Price < collection.Product[j].Price)
                {

                    productSwap = collection.Product[j];
                    collection.Product[j] = collection.Product[j - 1];
                    collection.Product[j - 1] = productSwap;
                    j--;
                }
            }
        }
    }





    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public decimal Price { get; set; }

        public Product(string name, string category = "", decimal price = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
        }

        public Product()
        {

        }
    }

    public class ProductCollection
    {
        public List<Product> Product { get; set; }

        public ProductCollection()
        {
            Product = DataLayer.Load<Product>();
        }

        public void Save()
        {
            DataLayer.Save(Product);
        }
    }
}
}
