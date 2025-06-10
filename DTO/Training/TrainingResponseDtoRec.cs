using WebApp.Entities;

namespace WebApp.DTO;

// move to separate classes
public record StrParams(int Set, int Weight, int Repetitions, double Volume);
public record StrExercise(StrengthExercise StrengthExercise, ICollection<StrParams> Params, double TotalExerciseVolume);
public record CarParams(int Interval, double Speed, TimeSpan Time, double Calories, TimeSpan TotalTime);
public record CarExercise(CardioExercise CardioExercise, IEnumerable<CarParams> Params, double Calories);

public record TrainingResponseDtoRec(
    int id,
    string Name,
    TimeSpan duration,
    DateOnly date,
    List<TrainingResponseDto.StrengthExerciseResponseDto> StrengthExercises,
    List<TrainingResponseDto.CarExerciseResponseDto> CardioExercises,
    double TotalCalories);