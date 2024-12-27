namespace WebApp.DTO;

public class UserResponseDto
{
    public int? Id { get; set; }
    public string? Username { get; set; }
    public string? Message { get; set; }

    public UserResponseDto(string message)
    {
        Message = message;
    }

    public UserResponseDto(int id, string username)
    {
        Id = id;
        Username = username;
    }
}