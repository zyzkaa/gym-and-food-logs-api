namespace WebApp.DTO.Meals;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CaloriesPer100g { get; set; }
    public double ProteinPer100g { get; set; }
    public double CarbsPer100g { get; set; }
    public double FatPer100g { get; set; }
}