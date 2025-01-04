using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<Training> AddTraining(TrainingDto trainingDto);
    public Task<List<Training>> GetTrainings();
    public Task<Training> GetTrainingById(int trainingId);
    public Task<List<Training>> GetTrainingsByDate(DateOnly date);
    public Task<StrengthExercise> GetStrengthExerciseById(int exerciseId);
    public Task<Training> DeleteTrainingById(int trainingId);
    public Task<CardioExercise> GetCardioExerciseById(int carExerciseId);
    public Task<List<StrengthExercise>> GetStrengthExercises();
    public Task<List<CardioExercise>> GetCardioExercises();
    //public TrainingResponseDto GetTrainingDetails(int trainingId);
    public Task<List<StrengthExercise>> GetExercisesByMuscleId(int muscleId);
    public Task<List<Training>> GetTrainingsByStrExerciseId(int exerciseId);
    //get training by exercise
}