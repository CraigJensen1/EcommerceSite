using EcommerceSite.Components.Storage;
namespace EcommerceSite.Components.Classes
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string username, string email, string password) 
        {
            Id = Guid.NewGuid();
            UserName = username;
            Email = email;
            Password = password;
        }
    }

    public class UserCollection
    {
        public List<User> Users { get; set; }

        public UserCollection()
        {
            Users = DataLayer.Load<User>();
        }

        public void Save()
        {
            DataLayer.Save(Users);
        }
    }
}
