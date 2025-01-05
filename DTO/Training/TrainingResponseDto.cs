using WebApp.Entities;

namespace WebApp.DTO;

public class TrainingResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan? Duration { get; set; } = TimeSpan.Zero;
    
    public record StrParamsResponseDto(int Set, int Weight, int Repetitions, double Volume);
    public record StrengthExerciseResponseDto(StrengthExercise StrengthExercise, ICollection<StrParamsResponseDto> StrParams, double Volume);

    public ICollection<StrengthExerciseResponseDto>? StrExercisesResponseDto { get; set; } =
        new List<StrengthExerciseResponseDto>();

    public record CarParamsResponseDto(int Interval, double Speed, TimeSpan Time, double Calories);
    public record CarExerciseResponseDto(CardioExercise CardioExercise, IEnumerable<CarParamsResponseDto> CarParams, double Calories);

    public ICollection<CarExerciseResponseDto>? CarExercisesResponseDto { get; set; } =
        new List<CarExerciseResponseDto>();
    public double TotalCalories { get; set; }
}