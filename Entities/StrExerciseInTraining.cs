using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class StrExerciseInTraining
{
    public int Id { get; set; }
    public StrengthExercise StrengthExercise { get; set; }
    public int Set { get; set; }
    public int Weight { get; set; }
    public int Repetitions { get; set; }
    
    public StrExerciseInTraining(){}
}