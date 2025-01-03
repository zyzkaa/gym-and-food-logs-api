namespace WebApp.Entities;

public class CarExerciseInTraining
{
    public int Id { get; set; }
    public CardioExercise CardioExercise { get; set; }
    public ICollection<CarParams> CarParams { get; set; } = new List<CarParams>();
}