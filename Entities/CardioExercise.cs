namespace WebApp.Entities;

public class CardioExercise : Exercise
{
    public ICollection<CarExerciseInTraining> CarExercisesInTrainings { get; set; }
    public ICollection<Met> Mets { get; set; }
}