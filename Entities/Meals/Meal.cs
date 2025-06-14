using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CreatorID {get; set;}
    public User Creator { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<MealIngredient> Ingredients { get; set; } = new List<MealIngredient>();

    // Właściwość obliczana
    public double CalculatedCalories => Ingredients.Sum(ingredient => ingredient.TotalCalories);
    public double CalculatedProtein => Ingredients.Sum(ingredient => ingredient.TotalProtein);
    public double CalculatedCarbs => Ingredients.Sum(ingredient => ingredient.TotalCarbs);
    public double CalculatedFat => Ingredients.Sum(ingredient => ingredient.TotalFat);

    // Relacja wiele-do-wielu
    public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
}
