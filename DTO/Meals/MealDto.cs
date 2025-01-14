namespace WebApp.DTO.Meals;

public class MealDto
{
    public string Name { get; set; }
    public List<MealIngredientDto> Ingredients { get; set; } = new List<MealIngredientDto>();
}