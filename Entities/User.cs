namespace WebApp.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? Weight { get; set; }
    public int? Height { get; set; }
    public int? Age { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}