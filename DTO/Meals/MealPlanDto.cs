using System.Text.Json.Serialization;

namespace WebApp.DTO.Meals;

public class MealPlanDto
{
    public DateTime Date { get; set; }
    public List<int> MealsID { get; set; }
}