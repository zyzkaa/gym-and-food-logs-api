namespace WebApp.Entities;

public class CarExerciseInTraining
{
    public int Id { get; set; }
    public CardioExercise CardioExercise { get; set; }
    public int Inteval { get; set; }
    public int Speed { get; set; }
    public TimeSpan Time { get; set; }
}