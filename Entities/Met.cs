namespace WebApp.Entities;

public class Met
{
    public int CardioExerciseId { get; set; }
    public CardioExercise CardioExercise { get; set; }
    public double StartSpeed  { get; set; }
    public double MetValue { get; set; }
}