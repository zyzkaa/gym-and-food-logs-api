using Swashbuckle.AspNetCore.Annotations;

namespace WebApp.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; }
}