using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace WebApp.DTO.Meals;

public class MealReturnDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public int CreatorID {get; set; }
    public DateTime CreatedAt { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<IngredientReturnDTO> Ingredients { get; set; }
}