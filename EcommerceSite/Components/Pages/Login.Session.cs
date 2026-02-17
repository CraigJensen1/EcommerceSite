using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using EcommerceSite.Components.Storage;
using System.Text;

namespace EcommerceSite.Pages
{
    public static class SessionStorage
    {
        public static Task LogoutAsync(IJSRuntime js) => SessionStorage.RemoveTokenAsync(js);

        public static Task<Guid?> GetUserIdFromStorageAsync(IJSRuntime js) => SessionStorage.GetUserIdFromTokenAsync(js);
        private const string TokenKey = "sessionToken";

        public static async Task SaveTokenAsync(IJSRuntime js, Guid userId)
        {
            var token = Convert.ToBase64String(userId.ToByteArray());
            await js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }
        public static async Task<Guid?> GetUserIdFromTokenAsync(IJSRuntime js)
        {
            try
            {
                var token = await js.InvokeAsync<string>("localStorage.getItem", TokenKey);
                if (string.IsNullOrEmpty(token)) return null;
                return new Guid(Convert.FromBase64String(token));
            }
            catch
            {
                return null;
            }
        }
        public static async Task RemoveTokenAsync(IJSRuntime js)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", TokenKey);

        }
        public static async Task<User?> GetUserFromStorageAsync(IJSRuntime js)
        {
            var uid = await GetUserIdFromStorageAsync(js);
            if (uid == null) return null;
            var users = new UserCollection();
            return users.FindById(uid.Value);
        }
    }

    // temporary code to implement user storage.

    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = "";
        public string UserName { get; set; } = "";
        public string PasswordHash { get; set; } = "";
    }

    public class UserCollection
    {
        private readonly List<User> _users;

        public UserCollection()
        {
            _users = DataLayer.Load<User>() ?? new List<User>();
        }

        public IEnumerable<User> Items => _users;

        public void Add(User u)
        {
            if (!_users.Any(x => x.Id == u.Id))
                _users.Add(u);
        }

        public void Save()
        {
            DataLayer.Save(_users);
        }

        public User? FindByEmail(string email)
        {
            return _users.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public User? FindById(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }
    }
}
