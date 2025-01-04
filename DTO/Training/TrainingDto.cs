using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingDto
{
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;

    public record StrParamsDto(int set, int weight, int repetitions);
    public record StrengthExerciseDto(int ExerciseId, IEnumerable<StrParamsDto> StrParams);
    public ICollection<StrengthExerciseDto> StrExercisesDto { get; set; }

    public record CarParamsDto(int interval, int speed, TimeSpan time);
    public record CarExerciseDto(int ExerciseId, IEnumerable<CarParamsDto> CarParams);
    public ICollection<CarExerciseDto> CarExercisesDto { get; set; }
}