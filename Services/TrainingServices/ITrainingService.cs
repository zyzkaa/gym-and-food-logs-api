using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public interface ITrainingService
{
    public Task<Training> AddTraining(Training training);
    // public Task<IEnumerable<Training>> GetTrainings();
    // public Task<Training> GetTrainingById(int trainingId);
    // public Task<IEnumerable<Training>> GetTrainingsByDate (DateOnly date);
}