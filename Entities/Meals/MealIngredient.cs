namespace WebApp.Entities;

public class MealIngredient
{
    public int Id { get; set; }
    public int Quantity { get; set; } // Ilość w gramach
    public Product Product { get; set; } 
    public Meal Meal { get; set; }

    public double TotalCalories => (Product.CaloriesPer100g * Quantity) / 100.0;
    public double TotalProtein => (Product.ProteinPer100g * Quantity) / 100.0;
    public double TotalCarbs => (Product.CarbsPer100g * Quantity) / 100.0;
    public double TotalFat => (Product.FatPer100g * Quantity) / 100.0;
}