namespace EcommerceSite.Components.Entities;

public class Categories
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Categories(string name, string description = "")
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }

    public class CategoriesCollection
    {
        public List<Categories> Categories { get; set; }
        public CategoriesCollection()
        {
            Categories = Storage.DataLayer.Load<Categories>();
        }
        public void Save()
        {
            Storage.DataLayer.Save(Categories);
        }
    }
}
