using EcommerceSite.Components.Storage;
namespace EcommerceSite.Components.Classes
{
    public class Permission
    {
        public string Name { get; set; }
        public string Description { get; set; }
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
