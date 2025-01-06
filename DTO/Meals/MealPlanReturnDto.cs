namespace WebApp.DTO.Meals;

public class MealPlanReturnDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
    public List<string> MealsName { get; set; }
}