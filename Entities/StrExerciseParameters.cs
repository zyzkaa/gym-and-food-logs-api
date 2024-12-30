namespace WebApp.Entities;

// zamienic na krotke??
// zamienic nazwe na set

public class StrExerciseParameters
{
    public int Set { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
    public int StrExerciseInTrainingStrengthExerciseId  { get; set; }
    public int StrExerciseInTrainingTrainingId { get; set; }
    public StrExerciseInTraining StrExerciseInTraining { get; set; }

    public StrExerciseParameters(int set, int weight, int repetitions)
    {
        Set = set;
        Weight = weight;
        Repetitions = repetitions;
    }
}