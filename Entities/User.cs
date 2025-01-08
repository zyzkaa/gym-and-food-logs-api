using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Weight is required")]
    [Range(30, 300, ErrorMessage = "Weight must be between 30 and 300 kg")]
    public int Weight { get; set; }

    [Required(ErrorMessage = "Height is required")]
    [Range(100, 250, ErrorMessage = "Height must be between 100 and 250 cm")]
    public int Height { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120 years")]
    public int Age { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [SwaggerSchema(ReadOnly = true)]
    public DateTime? ModifiedAt { get; set; }
    
    [SwaggerSchema(ReadOnly = true)]
    public ICollection<Training>? Trainings { get; set; }
}