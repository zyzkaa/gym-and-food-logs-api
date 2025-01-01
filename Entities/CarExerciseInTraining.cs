namespace WebApp.Entities;

public class CarExerciseInTraining
{
    public int Id { get; set; }
    public CardioExercise CardioExercise { get; set; }
    public int Set { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
}