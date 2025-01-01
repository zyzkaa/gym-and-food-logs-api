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
    public string Username { get; set; }
    public string Password { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public DateTime? ModifiedAt { get; set; }
    [SwaggerSchema(ReadOnly = true)]
    public ICollection<Training>? Trainings { get; set; }
}