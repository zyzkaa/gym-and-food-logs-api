using WebApp.Entities;

namespace WebApp.DTO;

public record StrParams(int Set, int Weight, int Repetitions, double Volume);
public record StrExercise(StrengthExercise StrengthExercise, ICollection<StrParams> Params, double TotalExerciseVolume);
public record CarParams(int Interval, double Speed, TimeSpan Time, double Calories, TimeSpan TotalTime);
public record CarExercise(CardioExercise CardioExercise, IEnumerable<CarParams> Params, double Calories);

public record TrainingResponseDtoRec(
    int id,
    string Name,
    TimeSpan duration,
    DateOnly date,
    List<TrainingResponseDto.StrengthExerciseResponseDto> StrengthExercises, // przenies to wyzej do oddzielnych klas ok?
    List<TrainingResponseDto.CarExerciseResponseDto> CarExercises,
    double TotalCalories);