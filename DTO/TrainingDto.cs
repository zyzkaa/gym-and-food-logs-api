using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingDto
{
    [MaxLength(50)]
    public string Name { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public DateOnly date { get; set; }

    public record StrSet(int Weight, int Repetitions);
    public record StrExercise(string Name, IEnumerable<StrSet> Sets);
    public IEnumerable<StrExercise> StrExercises { get; set; }    
    
    public record CarSet(int Speed, int Seconds);
    public record CarExercise(string Name, IEnumerable<CarSet> Sets);
    public IEnumerable<CarExercise> CarExercises { get; set; }
}