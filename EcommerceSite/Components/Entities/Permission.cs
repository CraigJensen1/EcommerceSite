using EcommerceSite.Components.Storage;
namespace EcommerceSite.Components.Classes
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Permission(string name, string description = "") 
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }
    }

    public class PermissionCollection
    {
        public List<Permission> Permissions { get; set; }

        public PermissionCollection()
        {
            Permissions = DataLayer.Load<Permission>();
        }

        public void Save()
        {
            DataLayer.Save(Permissions);
        }
    }
}
