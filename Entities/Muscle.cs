namespace WebApp.Entities;

public class Muscle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<StrengthExercise> StrengthExercises { get; set; }
}