namespace WebApp.DTO.Meals;

public class MealDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageURL { get; set; }
    public bool IsShared { get; set; }
    public int? OriginalMealID { get; set; }
    public int CreatorId { get; set; }

    public List<MealIngredientDto> Ingredients { get; set; } = new List<MealIngredientDto>();


}