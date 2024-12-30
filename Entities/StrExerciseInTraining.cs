using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class StrExerciseInTraining
{
    public int TrainingId { get; set; }
    public int StrengthExerciseId { get; set; }

    public StrengthExercise StrengthExercise { get; set; }
    public Training Training { get; set; }
    public ICollection<StrExerciseParameters> StrExerciseParameters { get; set; } = new List<StrExerciseParameters>();
    
    public StrExerciseInTraining(){}
}