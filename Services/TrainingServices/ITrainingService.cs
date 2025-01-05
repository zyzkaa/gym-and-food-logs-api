using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<Training> AddTraining(TrainingDto trainingDto);
    public Task<List<Training>> GetTrainings();
    public Task<Training> GetTrainingById(int trainingId);
    public Task<List<Training>> GetTrainingsByDate(DateOnly date);
    public Task<Training> DeleteTrainingById(int trainingId);
    public Task<TrainingResponseDto> GetTrainingWithDetails(int trainingId);
    public Task<List<Training>> GetTrainingsByStrExerciseId(int exerciseId);
    public Task<List<Training>> GetTrainingsByCarExerciseId(int exerciseId);
}