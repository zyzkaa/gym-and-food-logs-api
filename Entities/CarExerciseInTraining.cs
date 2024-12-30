namespace WebApp.Entities;

public class CarExerciseInTraining
{
    public int TrainingId { get; set; }
    public int CardioExerciseId { get; set; }

    public CardioExercise CardioExercise { get; set; }
    public Training Training { get; set; }
    public ICollection<CarExerciseParameters> CarExerciseParameters { get; set; } = new List<CarExerciseParameters>();
}