using WebApp.DTO.Meals;

public class MealReturnDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }

    public int CreatorID { get; set; }
    public int? EditorID { get; set; }
    public DateTime CreatedAt { get; set; }

    // This holds the URL or relative path to the image file
    public string ImageURL { get; set; }

    public string? Description { get; set; }
    public int? OriginalMealID { get; set; }

    public List<IngredientReturnDTO> Ingredients { get; set; } = new();
    
}