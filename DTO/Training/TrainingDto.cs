using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; }
        
    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }
    
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;
    
    public record StrParamsDto(int Set, int Weight, int Repetitions);
    public record StrengthExerciseDto(int ExerciseId, IEnumerable<StrParamsDto> StrParams);
    public ICollection<StrengthExerciseDto>? StrExercisesDto { get; set; }

    public record CarParamsDto(int Interval, int Speed, TimeSpan Time);
    public record CarExerciseDto(int ExerciseId, IEnumerable<CarParamsDto> CarParams);
    public ICollection<CarExerciseDto>? CarExercisesDto { get; set; }
}