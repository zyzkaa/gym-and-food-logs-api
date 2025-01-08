using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto);
    public Task<List<Training>> GetTrainings();
    public Task<Training> GetTrainingById(int trainingId);
    public Task<List<Training>> GetTrainingsByDate(DateOnly date);
    public Task<Training> DeleteTrainingById(int trainingId);
    public Task<TrainingResponseDto> GetTrainingWithDetails(int trainingId);
    public Task<List<Training>> GetTrainingsByStrengthExerciseId(int exerciseId);
    public Task<List<Training>> GetTrainingsByCardioExerciseId(int exerciseId);
    public Task<List<Training>> GetTrainigsWithRecordsByStrengthExerciseId(int exerciseId);
}