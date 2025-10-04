using EcommerceSite.Components.Storage;
using System.Net;

namespace EcommerceSite.Components.Entities
{
    public class Users
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }




        public Users() { }

        public Users(string userName, string email = "", string password = "", string address = "")
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            Password = password;
            Address = address;

        }


        public Users Register(Users user)
        {

            user.Id = Guid.NewGuid();


            Console.WriteLine("What would you like to be your username?");
            string userName = Console.ReadLine();
            user.UserName = userName;

            Console.WriteLine("Please enter your Email.");
            string email = Console.ReadLine();
            user.Email = email;

            Console.WriteLine("Please write a password.");
            string password = Console.ReadLine();
            user.Password = password;

            Console.WriteLine("What is your address.");
            string address = Console.ReadLine();
            user.Address = address;

            return user;
        }

        public Users Login( string userName, string password)
        {
            Users user = new Users();
  
            UsersCollection collection = new UsersCollection();


            for (int i = 0; i < collection.Users.Count; i++)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    return user;
                }
            }
            if (user != null)
            {
                Register(user);
            }

                return user;

        }

        public void Update(Users user)
        {
            Console.WriteLine("What would you like to be your new username?");
            string userName = Console.ReadLine();
            user.UserName = userName;

            Console.WriteLine("Please enter your Email.");
            string email = Console.ReadLine();
            user.Email = email;

            Console.WriteLine("Please write a password.");
            string password = Console.ReadLine();
            user.Password = password;

            Console.WriteLine("What is your address.");
            string address = Console.ReadLine();
            user.Address = address;

        }
    }

    public class UsersCollection
    {
        public List<Users> Users { get; set; }

        public UsersCollection()
        {
            Users = DataLayer.Load<Users>();
        }

        public void Save()
        {
            DataLayer.Save(Users);
        }
    }

    //public enum Role
    //{
    //    Admin,
    //    Employee,
    //    Customer
    //}
}
