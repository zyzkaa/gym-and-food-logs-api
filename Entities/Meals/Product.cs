namespace WebApp.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CaloriesPer100g { get; set; } // Kalorie na 100g produktu
    public double ProteinPer100g { get; set; } // Białko na 100g
    public double CarbsPer100g { get; set; } // Węglowodany na 100g
    public double FatPer100g { get; set; } // Tłuszcz na 100g

    public ICollection<MealIngredient> MealIngredients { get; set; } = new List<MealIngredient>();
}
