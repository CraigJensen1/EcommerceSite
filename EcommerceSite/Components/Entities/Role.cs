using EcommerceSite.Components.Storage;

namespace EcommerceSite.Components.Classes
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Roles(string name, string description = "")
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
    }

    public class RolesCollection
    {
        public List<Roles> Roles { get; set; }

        public RolesCollection()
        {
            Roles = DataLayer.Load<Roles>();
        }

        public void Save()
        {
            DataLayer.Save(Roles);
        }
    }
}
