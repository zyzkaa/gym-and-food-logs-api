namespace WebApp.DTO.Meals;

public class MealDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MealIngredientDto> Ingredients { get; set; } = new List<MealIngredientDto>();
    public double CalculatedCalories { get; set; }
    public double CalculatedProtein { get; set; }
    public double CalculatedCarbs { get; set; }
    public double CalculatedFat { get; set; }
}