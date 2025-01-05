namespace WebApp.DTO.Meals;

public class MealPlanDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public List<int> MealsID { get; set; }
}