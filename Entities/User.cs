using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Entities;
[Index(nameof(Username), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    [MaxLength(20, ErrorMessage = "Username must be less than 20 characters")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public int? Weight { get; set; }
    public int? Height { get; set; }
    public int? Age { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? ModifiedAt { get; set; }
    
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