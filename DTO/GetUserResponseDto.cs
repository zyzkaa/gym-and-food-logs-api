using WebApp.Entities;

namespace WebApp.DTO;

// i moze jakosz zmiana do nazwy typu user with details dto bo juz jeden dto do usera jest
public class GetUserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // imo do wywalenia bo sie nie powinno zwacac hasla :/
    public int Weight { get; set; }
    public int Height { get; set; }
    public int Age { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Dictionary<string,string[]>? Trainings { get; set; }
    public List<string[]>? MealPlans { get; set; }
}