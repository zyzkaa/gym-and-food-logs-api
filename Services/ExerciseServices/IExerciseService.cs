using WebApp.Entities;

namespace WebApp.Services.ExerciseServices;

public interface IExerciseService
{
    public Task<StrengthExercise> GetStrengthExerciseById(int exerciseId);
    public Task<CardioExercise> GetCardioExerciseById(int carExerciseId);
    public Task<List<StrengthExercise>> GetStrengthExercises();
    public Task<List<CardioExercise>> GetCardioExercises();
    public Task<List<StrengthExercise>> GetExercisesByMuscleId(int muscleId);
    public Task<List<CardioExercise>> GetCardioExercisesBySearch(string name);
    public Task<List<StrengthExercise>> GetStrExercisesBySearch(string name);
}