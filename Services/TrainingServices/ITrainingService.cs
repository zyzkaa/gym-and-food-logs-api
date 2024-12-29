using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto);
    public Task<TrainingResponseDto> GetTrainings();
    public Task<TrainingResponseDto> StartTraining(TrainingDto trainingDto);
    public Task<TrainingResponseDto> StopTraining(TrainingDto trainingDto);
    public Task<TrainingResponseDto> SetCurrentTraining(int id);
    public TrainingResponseDto SetCurrentTraining(Training training);
    public Task<TrainingResponseDto> AddExercise(Exercise exercise);
    public Training GetCurrentTraining();
}