using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities;

public class Training
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; }
    public ICollection<StrExerciseInTraining> StrExercises { get; set; }
    public ICollection<CarExerciseInTraining> CarExercises { get; set; }
    
    public Training(){}
}   