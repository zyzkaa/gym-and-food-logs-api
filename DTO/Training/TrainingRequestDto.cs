using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
    public string Name { get; set; }
        
    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }
    
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;
    
    public record StrengthParamsDto(int Set, int Weight, int Repetitions);
    public record StrengthExerciseDto(int ExerciseId, IEnumerable<StrengthParamsDto> Params);
    public ICollection<StrengthExerciseDto>? StrengthExercises { get; set; }
    
    public record CardioParamsDto(int Interval, double Speed, TimeSpan Time);
    public record CardioExerciseDto(int ExerciseId, IEnumerable<CardioParamsDto> Params);
    public ICollection<CardioExerciseDto>? CardioExercises { get; set; }
}