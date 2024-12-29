using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

//dodaj atrybuty 

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public int Age { get; set; }
}