using EcommerceSite.Components.Classes;
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using System.Text;
namespace EcommerceSite.Pages;

public partial class NewUser : ComponentBase
{
    private UserCollection users = new UserCollection();
    private string email = string.Empty;
    private string password = string.Empty;
    private string reEnterPassword = string.Empty;
    private string username = string.Empty;

    public void AddUser()
    {
        if (VerifyEmail() && VerifyPassword() && VerifyUsername())
        {
            Console.WriteLine("If Statement");
            HashPassword();
            users.Users.Add(new User(username, email, password));
            users.Save();
        }

        Console.WriteLine("After if statement");
        email = string.Empty;
        password = string.Empty;
        reEnterPassword = string.Empty;
        username = string.Empty;
        
    }

    private void HashPassword()
    {
        SHA256 sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
        password = Encoding.UTF8.GetString(hashedPasswordBytes);
    }


    private bool VerifyEmail()
    {
        foreach (User user in users.Users)
        {
            if (user.Email == email)
                return false;
        }
        if (email.Contains('@') && email.Contains('.'))
                return true;
        return false;

    }
    private bool VerifyPassword()
    {
        if (password == reEnterPassword)
            return true;
        return false;
    }
    private bool VerifyUsername()
    {
        foreach (User user in users.Users)
        {
            if (user.UserName == username)
                return false;
        }
        return true;
    }
}