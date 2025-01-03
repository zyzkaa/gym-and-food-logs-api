namespace WebApp.DTO.Meals;

public class MealIngredientDto
{
    public int Id { get; set; }
    public int Quantity { get; set; } // Ilość w gramach
    public ProductDto Product { get; set; }
    public double TotalCalories { get; set; }
    public double TotalProtein { get; set; }
    public double TotalCarbs { get; set; }
    public double TotalFat { get; set; }
}