using Microsoft.AspNetCore.Components;
using EcommerceSite.Components.Classes;

namespace EcommerceSite.Pages
{
    public partial class Permissions : ComponentBase
    {
        private PermissionCollection permissions = new PermissionCollection();

        private string newName = string.Empty;
        private string newDescription = string.Empty;

        private void AddPermission()
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                permissions.Permissions.Add(new Permission(newName, newDescription));

                permissions.Save();

                newName = string.Empty;
                newDescription = string.Empty;
            }
        }

        private void DeletePermission(Permission perm)
        {
            if (permissions.Permissions.Contains(perm))
            {
                permissions.Permissions.Remove(perm);
                permissions.Save();
            }
        }
    }
}
