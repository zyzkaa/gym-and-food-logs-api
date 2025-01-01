using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public class TrainingService : ITrainingService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TrainingService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
        
    public async Task<Training> AddTraining(Training training)
    {
        _dbContext.Trainings.Add(training);
        _dbContext.SaveChanges();
        return training;
    }

    public async Task<IEnumerable<Training>> GetTrainings()
    {
        throw new NotImplementedException();
    }

    public Task<Training> GetTrainingById(int trainingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Training>> GetTrainingsByDate(DateOnly date)
    {
        throw new NotImplementedException();
    }
}