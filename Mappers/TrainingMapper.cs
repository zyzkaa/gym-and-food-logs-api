using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Mappers;

public static class TrainingMapper
{
    public static TrainingShortResponseDto ToShortResponseDto(this Training training)
    {
        var exerciseList = new List<String>();
        foreach (var exercise in training.CardioExercises)
        {
            exerciseList.Add(exercise.CardioExercise.Name);
        }
        foreach (var exercise in training.StrengthExercises)
        {
            exerciseList.Add(exercise.StrengthExercise.Name);
        }

        return new TrainingShortResponseDto(
            training.Id,
            training.Name,
            training.Date,
            training.Duration ?? TimeSpan.Zero,
            exerciseList);
    }
}