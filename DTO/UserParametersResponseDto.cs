namespace WebApp.DTO;

public class UserParametersResponseDto
{
    public UserParametersResponseDto(int weight, int height, int age)
    {
        Weight = weight;
        Height = height;
        Age = age;
    }

    public UserParametersResponseDto(string message)
    {
        Message = message;
    }

    public int? Weight { get; set; }
    public int? Height { get; set; }
    public int? Age { get; set; }
    public string? Message { get; set; }
}