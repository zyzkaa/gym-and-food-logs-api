using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingDto
{
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;

    public record StrExercise(int ExerciseId, int Set, int Weight, int Repetitions);
    public ICollection<StrExercise> StrExercises { get; set; }
    
    public record CarExercise(int ExerciseId, int Interval, int Speed, TimeSpan Time);
    public ICollection<CarExercise> CarExercises { get; set; }
}