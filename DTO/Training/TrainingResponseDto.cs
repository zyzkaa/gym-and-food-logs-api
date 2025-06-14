using System.Text.Json.Serialization;
using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;
    public DateOnly Date { get; set; }
    
    public record StrParamsResponseDto(int Set, double Weight, int Repetitions, double Volume);
    public record StrengthExerciseResponseDto(StrengthExercise StrengthExercise, ICollection<StrParamsResponseDto> Params, double TotalExerciseVolume);

    public ICollection<StrengthExerciseResponseDto>? StrengthExercises { get; set; } =
        new List<StrengthExerciseResponseDto>();

    public record CarParamsResponseDto(int Interval, double Speed, TimeSpan Time, double Calories);
    public record CarExerciseResponseDto(CardioExercise CardioExercise, IEnumerable<CarParamsResponseDto> Params, double Calories, TimeSpan Time);

    public ICollection<CarExerciseResponseDto>? CardioExercises { get; set; } =
        new List<CarExerciseResponseDto>();
    public double TotalCalories { get; set; }
}