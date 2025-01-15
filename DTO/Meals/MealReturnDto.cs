using System.Text.Json.Serialization;

namespace WebApp.DTO.Meals;

public class MealReturnDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Calories { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Ingredients { get; set; }
}