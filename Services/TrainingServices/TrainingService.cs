using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Entities;

namespace WebApp.Services.TrainingServices;

public class TrainingService : ITrainingService
{
    private readonly WebAppContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICurrentTrainingSerivce _currentTrainingService;

    public TrainingService(WebAppContext dbContext, IHttpContextAccessor httpContextAccessor, ICurrentTrainingSerivce currentTrainingService)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _currentTrainingService = currentTrainingService;
    }
        
    public async Task<TrainingResponseDto> AddTraining(TrainingDto trainingDto)
    {
        DateTime date = DateTime.Parse(_httpContextAccessor.HttpContext.Session.GetString("date"));
        date += new TimeSpan(trainingDto.StartHour, trainingDto.StartMinute, 0);
        TimeSpan duration = new TimeSpan(trainingDto.Hours, trainingDto.Minutes, 0);
        string currentUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(currentUser));
        
        var newTraining = new Training(trainingDto.Name, date, duration, user);
        
        _dbContext.Trainings.Add(newTraining);
        await _dbContext.SaveChangesAsync();
        SetCurrentTraining(newTraining);
        
        return new TrainingResponseDto(newTraining.Id, newTraining.Name);
    }

    public Task<TrainingResponseDto> GetTrainings()
    {
        throw new NotImplementedException();
    }

    public Task<TrainingResponseDto> StartTraining(TrainingDto trainingDto)
    {
        throw new NotImplementedException();
    }

    public Task<TrainingResponseDto> StopTraining(TrainingDto trainingDto)
    {
        throw new NotImplementedException();
    }

    public async Task<TrainingResponseDto> SetCurrentTraining(int id)
    {
        var currentTraining = await _dbContext.Trainings.FirstOrDefaultAsync(t => t.Id == id);
        _currentTrainingService.SetCurrentTraining(currentTraining);
        return new TrainingResponseDto(currentTraining.Id, currentTraining.Name);
    }

    public TrainingResponseDto SetCurrentTraining(Training training)
    {
        _currentTrainingService.SetCurrentTraining(training);
        return new TrainingResponseDto(training.Id, training.Name);
    }

    public Task<TrainingResponseDto> AddExercise(Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Training GetCurrentTraining()
    {
        return _currentTrainingService.GetCurrentTraining();
    }
}