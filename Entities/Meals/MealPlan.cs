namespace WebApp.Entities;

public class MealPlan
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; }
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
}