using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Entities;
[Index(nameof(Username), IsUnique = true)]
public class User
{
    public new int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    
    public User() {}

    public User(string username, string password, int weight, int height, int age, DateTime createdAt)
    {
        Username = username;
        Password = password;
        Weight = weight;
        Height = height;
        Age = age;
        CreatedAt = createdAt;
    }
}