using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<TrainingShortResponseDto> AddTraining(TrainingRequestDto trainingRequestDto);
    public Task<List<Training>> GetTrainings(int page, int amount);
    public Task<Training> GetTrainingById(int trainingId);
    public Task<List<Training>> GetTrainingsByDate(DateOnly date);
    public Task<Training> DeleteTrainingById(int trainingId);
    public Task<TrainingResponseDtoRec?> GetTrainingWithDetails(int trainingId);
    public Task<List<Training>> GetTrainingsByStrengthExerciseId(int exerciseId);
    public Task<List<Training>> GetTrainingsByCardioExerciseId(int exerciseId);
    public Task<List<Training>> GetTrainigsWithRecordsByStrengthExerciseId(int exerciseId);
}