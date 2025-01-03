namespace WebApp.DTO.Meals;

public class MealPlanDto
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int UserId { get; set; }
    public List<MealDto> Meals { get; set; } = new List<MealDto>();
}