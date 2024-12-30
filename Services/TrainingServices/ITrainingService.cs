using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto);
    public Task<TrainingResponseDto> GetTrainings();
    public Task<TrainingResponseDto> GetTrainingById(int trainingId);
}