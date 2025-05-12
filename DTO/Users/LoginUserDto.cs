using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO;

public class LoginUserDto
{
    [Required (ErrorMessage = "Username is required")]
    [MinLength(1)]
    public string Username { get; set; }
    [Required (ErrorMessage = "Password is required")]
    [MinLength(1)]
    public string Password { get; set; }

    public LoginUserDto(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }
}