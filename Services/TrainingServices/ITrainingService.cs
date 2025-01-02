using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<Training> AddTraining(TrainingDto trainingDto);
    public Task<IEnumerable<Training>> GetTrainings();
    public Task<Training> GetTrainingById(int trainingId);
    public Task<IEnumerable<Training>> GetTrainingsByDate (DateOnly date);
    public Task<StrengthExercise> GetStrengthExerciseById(int exerciseId);
    public Task<Training> DeleteTrainingById(int trainingId);
}